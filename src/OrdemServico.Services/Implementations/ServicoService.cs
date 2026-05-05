using System;
using System.Collections.Generic;
using OrdemServico.Entities;
using OrdemServico.Infra.Database;
using OrdemServico.Infra.Logging;
using OrdemServico.Repositories.Implementations;
using OrdemServico.Services.Exceptions;

namespace OrdemServico.Services.Implementations
{
    public class ServicoService
    {
        private readonly ServicoRepository _repo = new ServicoRepository();

        public long Cadastrar(Servico servico)
        {
            Validar(servico);

            using (var conn = ConnectionFactory.CreateOpenConnection())
            using (var tx = conn.BeginTransaction())
            {
                try
                {
                    var id = _repo.Inserir(servico, conn, tx);
                    tx.Commit();
                    Logger.Info("Servico cadastrado: id=" + id, "ServicoService.Cadastrar");
                    return id;
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    Logger.Error("Erro ao cadastrar servico", ex, "ServicoService.Cadastrar");
                    throw;
                }
            }
        }

        public void Atualizar(Servico servico)
        {
            Validar(servico);

            using (var conn = ConnectionFactory.CreateOpenConnection())
            using (var tx = conn.BeginTransaction())
            {
                try
                {
                    var existente = _repo.ObterPorId(servico.Id, conn, tx);
                    if (existente == null)
                        throw new EntidadeNaoEncontradaException("Servico nao encontrado.");

                    _repo.Atualizar(servico, conn, tx);
                    tx.Commit();
                    Logger.Info("Servico atualizado: id=" + servico.Id, "ServicoService.Atualizar");
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    Logger.Error("Erro ao atualizar servico", ex, "ServicoService.Atualizar");
                    throw;
                }
            }
        }

        public Servico ObterPorId(long id)
        {
            using (var conn = ConnectionFactory.CreateOpenConnection())
            {
                return _repo.ObterPorId(id, conn);
            }
        }

        public List<Servico> ListarAtivos()
        {
            using (var conn = ConnectionFactory.CreateOpenConnection())
            {
                return _repo.ListarAtivos(conn);
            }
        }

        public List<Servico> ListarTodos()
        {
            using (var conn = ConnectionFactory.CreateOpenConnection())
            {
                return _repo.ListarTodos(conn);
            }
        }

        private void Validar(Servico s)
        {
            if (s == null)
                throw new RegraNegocioException("Servico nao pode ser nulo.");
            if (string.IsNullOrWhiteSpace(s.Nome))
                throw new RegraNegocioException("Nome do servico e obrigatorio.");
            if (s.ValorBase <= 0)
                throw new RegraNegocioException("Valor base deve ser maior que zero.");
            if (s.PercentualImposto < 0 || s.PercentualImposto > 100)
                throw new RegraNegocioException("Percentual de imposto deve estar entre 0 e 100.");
        }
    }
}