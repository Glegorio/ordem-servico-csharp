using System;
using System.Collections.Generic;
using System.Text;
using Npgsql;
using OrdemServico.Entities.Enums;
using OrdemServico.Infra.Database;
using OrdemServico.Infra.Logging;
using OrdemServico.Reports.Dtos;
using OrdemServico.Reports.Filters;

namespace OrdemServico.Reports.Services
{
    /// <summary>
    /// Service responsável por gerar os dados do relatório gerencial.
    /// Aplica filtros dinamicamente e calcula o valor
    /// de imposto via SQL (somatório dos itens).
    /// </summary>
    public class RelatorioService
    {
        public List<RelatorioOrdemDto> GerarDadosOrdens(RelatorioFiltro filtro)
        {
            if (filtro == null) filtro = new RelatorioFiltro();

            var sql = new StringBuilder();
            sql.Append(@"
                SELECT
                    o.id                                      AS ordem_id,
                    o.data_abertura,
                    o.data_conclusao,
                    o.status,
                    c.id                                      AS cliente_id,
                    c.nome                                    AS cliente_nome,
                    c.documento                               AS cliente_documento,
                    o.valor_total,
                    COALESCE((
                        SELECT SUM(
                            i.quantidade * i.valor_unitario *
                            (i.percentual_imposto_aplicado / 100.0)
                        )
                        FROM itens_ordem_servico i
                        WHERE i.ordem_servico_id = o.id
                    ), 0)                                     AS valor_imposto
                FROM ordens_servico o
                JOIN clientes c ON c.id = o.cliente_id
                WHERE 1=1 ");

            var parametros = new List<NpgsqlParameter>();

            if (filtro.DataInicio.HasValue)
            {
                sql.Append(" AND o.data_abertura >= @data_inicio ");
                parametros.Add(new NpgsqlParameter("@data_inicio", filtro.DataInicio.Value));
            }

            if (filtro.DataFim.HasValue)
            {
                sql.Append(" AND o.data_abertura <= @data_fim ");
                parametros.Add(new NpgsqlParameter("@data_fim", filtro.DataFim.Value));
            }

            if (filtro.ClienteId.HasValue)
            {
                sql.Append(" AND o.cliente_id = @cliente_id ");
                parametros.Add(new NpgsqlParameter("@cliente_id", filtro.ClienteId.Value));
            }

            if (filtro.Status.HasValue)
            {
                sql.Append(" AND o.status = @status::status_os_enum ");
                parametros.Add(new NpgsqlParameter("@status", StatusToDb(filtro.Status.Value)));
            }

            sql.Append(" ORDER BY c.nome, o.data_abertura; ");

            var lista = new List<RelatorioOrdemDto>();

            try
            {
                using (var conn = ConnectionFactory.CreateOpenConnection())
                using (var cmd = new NpgsqlCommand(sql.ToString(), conn))
                {
                    foreach (var p in parametros) cmd.Parameters.Add(p);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new RelatorioOrdemDto
                            {
                                OrdemId = reader.GetInt64(reader.GetOrdinal("ordem_id")),
                                DataAbertura = reader.GetDateTime(reader.GetOrdinal("data_abertura")),
                                DataConclusao = reader.IsDBNull(reader.GetOrdinal("data_conclusao"))
                                    ? (DateTime?)null
                                    : reader.GetDateTime(reader.GetOrdinal("data_conclusao")),
                                Status = StatusFromDbAmigavel(reader.GetString(reader.GetOrdinal("status"))),
                                ClienteId = reader.GetInt64(reader.GetOrdinal("cliente_id")),
                                ClienteNome = reader.GetString(reader.GetOrdinal("cliente_nome")),
                                ClienteDocumento = reader.GetString(reader.GetOrdinal("cliente_documento")),
                                ValorTotal = reader.GetDecimal(reader.GetOrdinal("valor_total")),
                                ValorImposto = reader.GetDecimal(reader.GetOrdinal("valor_imposto"))
                            });
                        }
                    }
                }

                Logger.Info("Relatorio gerado: " + lista.Count + " registros", "RelatorioService.GerarDadosOrdens");
            }
            catch (Exception ex)
            {
                Logger.Error("Erro ao gerar relatorio", ex, "RelatorioService.GerarDadosOrdens");
                throw;
            }

            return lista;
        }

        private static string StatusToDb(StatusOS status)
        {
            switch (status)
            {
                case StatusOS.Aberta: return "ABERTA";
                case StatusOS.EmAndamento: return "EM_ANDAMENTO";
                case StatusOS.Concluida: return "CONCLUIDA";
                case StatusOS.Cancelada: return "CANCELADA";
                default: throw new ArgumentException("Status invalido");
            }
        }

        private static string StatusFromDbAmigavel(string s)
        {
            switch (s)
            {
                case "ABERTA": return "Aberta";
                case "EM_ANDAMENTO": return "Em andamento";
                case "CONCLUIDA": return "Concluida";
                case "CANCELADA": return "Cancelada";
                default: return s;
            }
        }
    }
}