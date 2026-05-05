using System;
using OrdemServico.Entities;
using OrdemServico.Infra.Database;
using OrdemServico.Infra.Logging;
using OrdemServico.Repositories.Common;
using OrdemServico.Repositories.Implementations;
using OrdemServico.Services.Exceptions;

namespace OrdemServico.Services.Implementations
{
    public class ClienteService
    {
        private readonly ClienteRepository _repo = new ClienteRepository();

        public long Cadastrar(Cliente cliente)
        {
            ValidarCliente(cliente);

            using (var conn = ConnectionFactory.CreateOpenConnection())
            using (var tx = conn.BeginTransaction())
            {
                try
                {
                    var id = _repo.Inserir(cliente, conn, tx);
                    tx.Commit();
                    Logger.Info("Cliente cadastrado: id=" + id, "ClienteService.Cadastrar");
                    return id;
                }
                catch (Npgsql.PostgresException ex)
                {
                    tx.Rollback();
                    Logger.Error("Erro ao cadastrar cliente", ex, "ClienteService.Cadastrar");

                    if (ex.SqlState == "23505") // unique_violation
                        throw new RegraNegocioException("Ja existe um cliente com esse documento.");
                    throw;
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    Logger.Error("Erro ao cadastrar cliente", ex, "ClienteService.Cadastrar");
                    throw;
                }
            }
        }

        public void Atualizar(Cliente cliente)
        {
            ValidarCliente(cliente);

            using (var conn = ConnectionFactory.CreateOpenConnection())
            using (var tx = conn.BeginTransaction())
            {
                try
                {
                    var existente = _repo.ObterPorId(cliente.Id, conn, tx);
                    if (existente == null)
                        throw new EntidadeNaoEncontradaException("Cliente nao encontrado.");

                    _repo.Atualizar(cliente, conn, tx);
                    tx.Commit();
                    Logger.Info("Cliente atualizado: id=" + cliente.Id, "ClienteService.Atualizar");
                }
                catch (Npgsql.PostgresException ex)
                {
                    tx.Rollback();
                    Logger.Error("Erro ao atualizar cliente", ex, "ClienteService.Atualizar");

                    if (ex.SqlState == "23505")
                        throw new RegraNegocioException("Ja existe outro cliente com esse documento.");
                    throw;
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    Logger.Error("Erro ao atualizar cliente", ex, "ClienteService.Atualizar");
                    throw;
                }
            }
        }

        public void Excluir(long id)
        {
            using (var conn = ConnectionFactory.CreateOpenConnection())
            using (var tx = conn.BeginTransaction())
            {
                try
                {
                    /// Não permitir exclusao se houver OS vinculada
                    var totalOS = _repo.ContarOSVinculadas(id, conn, tx);
                    if (totalOS > 0)
                        throw new RegraNegocioException(
                            "Nao e possivel excluir o cliente pois ha " + totalOS +
                            " ordem(ns) de servico vinculada(s).");

                    _repo.Excluir(id, conn, tx);
                    tx.Commit();
                    Logger.Info("Cliente excluido: id=" + id, "ClienteService.Excluir");
                }
                catch (RegraNegocioException)
                {
                    tx.Rollback();
                    throw;
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    Logger.Error("Erro ao excluir cliente", ex, "ClienteService.Excluir");
                    throw;
                }
            }
        }

        public Cliente ObterPorId(long id)
        {
            using (var conn = ConnectionFactory.CreateOpenConnection())
            {
                return _repo.ObterPorId(id, conn);
            }
        }

        public PagedResult<Cliente> Buscar(ClienteFiltro filtro, int pagina, int tamanhoPagina)
        {
            if (pagina < 1) pagina = 1;
            if (tamanhoPagina < 1) tamanhoPagina = 20;

            using (var conn = ConnectionFactory.CreateOpenConnection())
            {
                return _repo.Buscar(filtro ?? new ClienteFiltro(), pagina, tamanhoPagina, conn);
            }
        }

        private void ValidarCliente(Cliente c)
        {
            if (c == null)
                throw new RegraNegocioException("Cliente nao pode ser nulo.");
            if (string.IsNullOrWhiteSpace(c.Nome))
                throw new RegraNegocioException("Nome e obrigatorio.");
            if (string.IsNullOrWhiteSpace(c.Documento))
                throw new RegraNegocioException("Documento e obrigatorio.");
        }
    }
}