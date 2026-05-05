using System;
using Newtonsoft.Json;
using Npgsql;
using OrdemServico.Entities;
using OrdemServico.Entities.Enums;
using OrdemServico.Infra.Database;
using OrdemServico.Infra.Logging;
using OrdemServico.Infra.Session;
using OrdemServico.Repositories.Common;
using OrdemServico.Repositories.Implementations;
using OrdemServico.Services.Exceptions;
using OS = OrdemServico.Entities.OrdemServico;

namespace OrdemServico.Services.Implementations
{
    /// <summary>
    /// Service da Ordem de Serviço. Concentra:
    /// - Regras de negócio (transições de status, validações)
    /// - Controle transacional (todas as operações em uma transação)
    /// - Concorrência otimista (campo versao)
    /// - Auditoria (snapshot JSON após cada alteração)
    /// </summary>
    public class OrdemServicoService
    {
        private readonly OrdemServicoRepository _osRepo = new OrdemServicoRepository();
        private readonly ItemOrdemServicoRepository _itemRepo = new ItemOrdemServicoRepository();
        private readonly HistoricoStatusRepository _histRepo = new HistoricoStatusRepository();
        private readonly AuditoriaRepository _audRepo = new AuditoriaRepository();
        private readonly ServicoRepository _servicoRepo = new ServicoRepository();

        // ABRIR NOVA OS
        public long Abrir(long clienteId, string observacao)
        {
            using (var conn = ConnectionFactory.CreateOpenConnection())
            using (var tx = conn.BeginTransaction())
            {
                try
                {
                    var os = new OS
                    {
                        ClienteId = clienteId,
                        DataAbertura = DateTime.Now,
                        Status = StatusOS.Aberta,
                        Observacao = observacao,
                        ValorTotal = 0,
                        Versao = 1
                    };

                    var id = _osRepo.Inserir(os, conn, tx);
                    os.Id = id;

                    _histRepo.Inserir(new HistoricoStatus
                    {
                        OrdemServicoId = id,
                        StatusAnterior = null,
                        StatusNovo = StatusOS.Aberta,
                        DataHora = DateTime.Now,
                        Usuario = UsuarioAtual()
                    }, conn, tx);

                    Auditar("ordens_servico", id, "INSERT", os, conn, tx);

                    tx.Commit();
                    Logger.Info("OS aberta: id=" + id, "OrdemServicoService.Abrir");
                    return id;
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    Logger.Error("Erro ao abrir OS", ex, "OrdemServicoService.Abrir");
                    throw;
                }
            }
        }

        // ADICIONAR ITEM
        public void AdicionarItem(long osId, long servicoId, decimal quantidade, int versaoAtual)
        {
            if (quantidade <= 0)
                throw new RegraNegocioException("Quantidade deve ser maior que zero.");

            using (var conn = ConnectionFactory.CreateOpenConnection())
            using (var tx = conn.BeginTransaction())
            {
                try
                {
                    var os = _osRepo.ObterPorId(osId, conn, tx);
                    if (os == null)
                        throw new EntidadeNaoEncontradaException("OS nao encontrada.");

                    ValidarStatusEditavel(os);

                    var servico = _servicoRepo.ObterPorId(servicoId, conn, tx);
                    if (servico == null)
                        throw new EntidadeNaoEncontradaException("Servico nao encontrado.");
                    if (!servico.Ativo)
                        throw new RegraNegocioException("Servico inativo nao pode ser adicionado.");

                    // Calcula o valor do item com valores CONGELADOS do serviço
                    var subtotal = quantidade * servico.ValorBase;
                    var imposto = subtotal * (servico.PercentualImposto / 100m);
                    var totalItem = subtotal + imposto;

                    var item = new ItemOrdemServico
                    {
                        OrdemServicoId = osId,
                        ServicoId = servicoId,
                        Quantidade = quantidade,
                        ValorUnitario = servico.ValorBase,           // CONGELADO
                        PercentualImpostoAplicado = servico.PercentualImposto, // CONGELADO
                        ValorTotalItem = totalItem
                    };

                    _itemRepo.Inserir(item, conn, tx);

                    // Recalcula valor total da OS
                    os.ValorTotal += totalItem;
                    os.Versao = versaoAtual;

                    var novaVersao = _osRepo.Atualizar(os, conn, tx);
                    if (novaVersao == 0)
                        throw new ConcorrenciaException(
                            "Esta OS foi alterada por outro usuario. Recarregue e tente novamente.");

                    os.Versao = novaVersao;
                    Auditar("ordens_servico", osId, "UPDATE", os, conn, tx);

                    tx.Commit();
                    Logger.Info("Item adicionado a OS " + osId, "OrdemServicoService.AdicionarItem");
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    if (!(ex is RegraNegocioException) && !(ex is ConcorrenciaException) && !(ex is EntidadeNaoEncontradaException))
                        Logger.Error("Erro ao adicionar item", ex, "OrdemServicoService.AdicionarItem");
                    throw;
                }
            }
        }

        // REMOVER ITEM
        public void RemoverItem(long osId, long itemId, int versaoAtual)
        {
            using (var conn = ConnectionFactory.CreateOpenConnection())
            using (var tx = conn.BeginTransaction())
            {
                try
                {
                    var os = _osRepo.ObterPorId(osId, conn, tx);
                    if (os == null)
                        throw new EntidadeNaoEncontradaException("OS nao encontrada.");

                    ValidarStatusEditavel(os);

                    var itens = _itemRepo.ListarPorOS(osId, conn, tx);
                    var item = itens.Find(i => i.Id == itemId);
                    if (item == null)
                        throw new EntidadeNaoEncontradaException("Item nao encontrado.");

                    _itemRepo.Excluir(itemId, conn, tx);

                    os.ValorTotal -= item.ValorTotalItem;
                    if (os.ValorTotal < 0) os.ValorTotal = 0;
                    os.Versao = versaoAtual;

                    var novaVersao = _osRepo.Atualizar(os, conn, tx);
                    if (novaVersao == 0)
                        throw new ConcorrenciaException(
                            "Esta OS foi alterada por outro usuario. Recarregue e tente novamente.");

                    os.Versao = novaVersao;
                    Auditar("ordens_servico", osId, "UPDATE", os, conn, tx);

                    tx.Commit();
                    Logger.Info("Item " + itemId + " removido da OS " + osId, "OrdemServicoService.RemoverItem");
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    if (!(ex is RegraNegocioException) && !(ex is ConcorrenciaException) && !(ex is EntidadeNaoEncontradaException))
                        Logger.Error("Erro ao remover item", ex, "OrdemServicoService.RemoverItem");
                    throw;
                }
            }
        }

        // ALTERAR STATUS
        public void AlterarStatus(long osId, StatusOS novoStatus, int versaoAtual)
        {
            using (var conn = ConnectionFactory.CreateOpenConnection())
            using (var tx = conn.BeginTransaction())
            {
                try
                {
                    var os = _osRepo.ObterPorId(osId, conn, tx);
                    if (os == null)
                        throw new EntidadeNaoEncontradaException("OS nao encontrada.");

                    ValidarTransicaoStatus(os.Status, novoStatus);

                    if (novoStatus == StatusOS.Concluida && os.ValorTotal == 0)
                        throw new RegraNegocioException("Nao e possivel concluir uma OS com valor total zero.");

                    var statusAnterior = os.Status;
                    os.Status = novoStatus;
                    os.Versao = versaoAtual;

                    if (novoStatus == StatusOS.Concluida)
                        os.DataConclusao = DateTime.Now;

                    var novaVersao = _osRepo.Atualizar(os, conn, tx);
                    if (novaVersao == 0)
                        throw new ConcorrenciaException(
                            "Esta OS foi alterada por outro usuario. Recarregue e tente novamente.");

                    os.Versao = novaVersao;

                    _histRepo.Inserir(new HistoricoStatus
                    {
                        OrdemServicoId = osId,
                        StatusAnterior = statusAnterior,
                        StatusNovo = novoStatus,
                        DataHora = DateTime.Now,
                        Usuario = UsuarioAtual()
                    }, conn, tx);

                    Auditar("ordens_servico", osId, "UPDATE", os, conn, tx);

                    tx.Commit();
                    Logger.Info("Status da OS " + osId + " alterado para " + novoStatus,
                        "OrdemServicoService.AlterarStatus");
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    if (!(ex is RegraNegocioException) && !(ex is ConcorrenciaException) && !(ex is EntidadeNaoEncontradaException))
                        Logger.Error("Erro ao alterar status", ex, "OrdemServicoService.AlterarStatus");
                    throw;
                }
            }
        }

        // CONSULTAS
        public OS ObterCompleta(long osId)
        {
            using (var conn = ConnectionFactory.CreateOpenConnection())
            {
                var os = _osRepo.ObterPorId(osId, conn);
                if (os == null)
                    throw new EntidadeNaoEncontradaException("OS nao encontrada.");

                os.Itens = _itemRepo.ListarPorOS(osId, conn);
                return os;
            }
        }

        public PagedResult<OS> Buscar(OrdemServicoFiltro filtro, int pagina, int tamanhoPagina)
        {
            if (pagina < 1) pagina = 1;
            if (tamanhoPagina < 1) tamanhoPagina = 20;

            using (var conn = ConnectionFactory.CreateOpenConnection())
            {
                return _osRepo.Buscar(filtro ?? new OrdemServicoFiltro(), pagina, tamanhoPagina, conn);
            }
        }

        // HELPERS PRIVADOS
        private void ValidarStatusEditavel(OS os)
        {
            if (os.Status == StatusOS.Concluida)
                throw new RegraNegocioException("OS concluida nao pode ser alterada.");
            if (os.Status == StatusOS.Cancelada)
                throw new RegraNegocioException("OS cancelada nao pode ser alterada.");
        }

        private void ValidarTransicaoStatus(StatusOS atual, StatusOS novo)
        {
            if (atual == novo)
                throw new RegraNegocioException("OS ja esta no status " + novo + ".");
            if (atual == StatusOS.Concluida)
                throw new RegraNegocioException("OS concluida nao pode mudar de status.");
            if (atual == StatusOS.Cancelada)
                throw new RegraNegocioException("OS cancelada nao pode mudar de status.");
        }

        private void Auditar(string entidade, long idRegistro, string operacao, object snapshot,
                             NpgsqlConnection conn, NpgsqlTransaction tx)
        {
            var reg = new RegistroAuditoria
            {
                Entidade = entidade,
                IdRegistro = idRegistro,
                Operacao = operacao,
                DataHora = DateTime.Now,
                Usuario = UsuarioAtual(),
                SnapshotJson = JsonConvert.SerializeObject(snapshot)
            };
            _audRepo.Inserir(reg, conn, tx);
        }

        private string UsuarioAtual()
        {
            return string.IsNullOrWhiteSpace(SessionContext.UsuarioAtual)
                ? "sistema"
                : SessionContext.UsuarioAtual;
        }
    }
}