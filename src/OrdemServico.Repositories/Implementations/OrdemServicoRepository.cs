using System;
using System.Collections.Generic;
using System.Text;
using Npgsql;
using OrdemServico.Entities;
using OrdemServico.Entities.Enums;
using OrdemServico.Repositories.Common;
using OS = OrdemServico.Entities.OrdemServico;

namespace OrdemServico.Repositories.Implementations
{
    /// <summary>
    /// Acesso a dados de Ordem de Serviço
    /// O método Atualizar implementa concorrência otimista via campo versao
    /// </summary>
    public class OrdemServicoRepository
    {
        public long Inserir(OS os, NpgsqlConnection conn, NpgsqlTransaction tx)
        {
            const string sql = @"
                INSERT INTO ordens_servico
                    (cliente_id, data_abertura, data_conclusao, status, observacao, valor_total, versao)
                VALUES
                    (@cliente_id, @data_abertura, @data_conclusao, @status::status_os_enum,
                     @observacao, @valor_total, @versao)
                RETURNING id;";

            using (var cmd = new NpgsqlCommand(sql, conn, tx))
            {
                cmd.Parameters.AddWithValue("@cliente_id", os.ClienteId);
                cmd.Parameters.AddWithValue("@data_abertura", os.DataAbertura);
                cmd.Parameters.AddWithValue("@data_conclusao",
                    os.DataConclusao.HasValue ? (object)os.DataConclusao.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@status", StatusToDb(os.Status));
                cmd.Parameters.AddWithValue("@observacao", (object)os.Observacao ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@valor_total", os.ValorTotal);
                cmd.Parameters.AddWithValue("@versao", os.Versao);

                return Convert.ToInt64(cmd.ExecuteScalar());
            }
        }

        /// <summary>
        /// Atualiza a OS aplicando concorrência otimista
        /// Retorna a nova versão. Se 0 linhas afetadas, lança ConcorrenciaException
        /// (a Service captura e converte em mensagem amigável)
        /// </summary>
        public int Atualizar(OS os, NpgsqlConnection conn, NpgsqlTransaction tx)
        {
            const string sql = @"
                UPDATE ordens_servico
                   SET cliente_id = @cliente_id,
                       data_conclusao = @data_conclusao,
                       status = @status::status_os_enum,
                       observacao = @observacao,
                       valor_total = @valor_total,
                       versao = versao + 1
                 WHERE id = @id
                   AND versao = @versao_atual
                RETURNING versao;";

            using (var cmd = new NpgsqlCommand(sql, conn, tx))
            {
                cmd.Parameters.AddWithValue("@id", os.Id);
                cmd.Parameters.AddWithValue("@cliente_id", os.ClienteId);
                cmd.Parameters.AddWithValue("@data_conclusao",
                    os.DataConclusao.HasValue ? (object)os.DataConclusao.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@status", StatusToDb(os.Status));
                cmd.Parameters.AddWithValue("@observacao", (object)os.Observacao ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@valor_total", os.ValorTotal);
                cmd.Parameters.AddWithValue("@versao_atual", os.Versao);

                var resultado = cmd.ExecuteScalar();
                if (resultado == null)
                    return 0; // 0 linhas afetadas — Service trata como conflito de concorrência

                return Convert.ToInt32(resultado);
            }
        }

        public OS ObterPorId(long id, NpgsqlConnection conn, NpgsqlTransaction tx = null)
        {
            const string sql = @"
                SELECT o.id, o.cliente_id, o.data_abertura, o.data_conclusao, o.status,
                       o.observacao, o.valor_total, o.versao, c.nome AS cliente_nome
                  FROM ordens_servico o
                  JOIN clientes c ON c.id = o.cliente_id
                 WHERE o.id = @id;";

            using (var cmd = new NpgsqlCommand(sql, conn, tx))
            {
                cmd.Parameters.AddWithValue("@id", id);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                        return Mapear(reader);
                }
            }
            return null;
        }

        public PagedResult<OS> Buscar(OrdemServicoFiltro filtro, int pagina, int tamanhoPagina,
                                       NpgsqlConnection conn, NpgsqlTransaction tx = null)
        {
            var where = new StringBuilder(" WHERE 1=1 ");
            var parametros = new List<NpgsqlParameter>();

            if (filtro.ClienteId.HasValue)
            {
                where.Append(" AND o.cliente_id = @cliente_id ");
                parametros.Add(new NpgsqlParameter("@cliente_id", filtro.ClienteId.Value));
            }

            if (filtro.Status.HasValue)
            {
                where.Append(" AND o.status = @status::status_os_enum ");
                parametros.Add(new NpgsqlParameter("@status", StatusToDb(filtro.Status.Value)));
            }

            if (filtro.DataInicio.HasValue)
            {
                where.Append(" AND o.data_abertura >= @data_inicio ");
                parametros.Add(new NpgsqlParameter("@data_inicio", filtro.DataInicio.Value));
            }

            if (filtro.DataFim.HasValue)
            {
                where.Append(" AND o.data_abertura <= @data_fim ");
                parametros.Add(new NpgsqlParameter("@data_fim", filtro.DataFim.Value));
            }

            // Total
            string sqlCount = "SELECT COUNT(*) FROM ordens_servico o" + where.ToString();
            int total;
            using (var cmd = new NpgsqlCommand(sqlCount, conn, tx))
            {
                foreach (var p in parametros) cmd.Parameters.Add(p.Clone());
                total = Convert.ToInt32(cmd.ExecuteScalar());
            }

            // Página 
            string sqlPagina = @"
                SELECT o.id, o.cliente_id, o.data_abertura, o.data_conclusao, o.status,
                       o.observacao, o.valor_total, o.versao, c.nome AS cliente_nome
                  FROM ordens_servico o
                  JOIN clientes c ON c.id = o.cliente_id" + where.ToString() + @"
                 ORDER BY o.data_abertura DESC
                 LIMIT @limit OFFSET @offset;";

            var resultado = new PagedResult<OS>
            {
                Pagina = pagina,
                TamanhoPagina = tamanhoPagina,
                TotalRegistros = total
            };

            using (var cmd = new NpgsqlCommand(sqlPagina, conn, tx))
            {
                foreach (var p in parametros) cmd.Parameters.Add(p);
                cmd.Parameters.AddWithValue("@limit", tamanhoPagina);
                cmd.Parameters.AddWithValue("@offset", (pagina - 1) * tamanhoPagina);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        resultado.Items.Add(Mapear(reader));
                }
            }

            return resultado;
        }

        private OS Mapear(NpgsqlDataReader reader)
        {
            return new OS
            {
                Id = reader.GetInt64(reader.GetOrdinal("id")),
                ClienteId = reader.GetInt64(reader.GetOrdinal("cliente_id")),
                DataAbertura = reader.GetDateTime(reader.GetOrdinal("data_abertura")),
                DataConclusao = reader.IsDBNull(reader.GetOrdinal("data_conclusao"))
                    ? (DateTime?)null
                    : reader.GetDateTime(reader.GetOrdinal("data_conclusao")),
                Status = StatusFromDb(reader.GetString(reader.GetOrdinal("status"))),
                Observacao = reader.IsDBNull(reader.GetOrdinal("observacao"))
                    ? null : reader.GetString(reader.GetOrdinal("observacao")),
                ValorTotal = reader.GetDecimal(reader.GetOrdinal("valor_total")),
                Versao = reader.GetInt32(reader.GetOrdinal("versao")),
                ClienteNome = reader.GetString(reader.GetOrdinal("cliente_nome"))
            };
        }

        private static string StatusToDb(StatusOS status)
        {
            switch (status)
            {
                case StatusOS.Aberta: return "ABERTA";
                case StatusOS.EmAndamento: return "EM_ANDAMENTO";
                case StatusOS.Concluida: return "CONCLUIDA";
                case StatusOS.Cancelada: return "CANCELADA";
                default: throw new ArgumentException("Status invalido: " + status);
            }
        }

        private static StatusOS StatusFromDb(string s)
        {
            switch (s)
            {
                case "ABERTA": return StatusOS.Aberta;
                case "EM_ANDAMENTO": return StatusOS.EmAndamento;
                case "CONCLUIDA": return StatusOS.Concluida;
                case "CANCELADA": return StatusOS.Cancelada;
                default: throw new ArgumentException("Status desconhecido do banco: " + s);
            }
        }
    }
}