using System;
using System.Collections.Generic;
using Npgsql;
using OrdemServico.Entities;
using OrdemServico.Entities.Enums;

namespace OrdemServico.Repositories.Implementations
{
    public class HistoricoStatusRepository
    {
        public void Inserir(HistoricoStatus h, NpgsqlConnection conn, NpgsqlTransaction tx)
        {
            const string sql = @"
                INSERT INTO historico_status_os
                    (ordem_servico_id, status_anterior, status_novo, data_hora, usuario)
                VALUES
                    (@os_id, @anterior::status_os_enum, @novo::status_os_enum, @data, @usuario);";

            using (var cmd = new NpgsqlCommand(sql, conn, tx))
            {
                cmd.Parameters.AddWithValue("@os_id", h.OrdemServicoId);
                cmd.Parameters.AddWithValue("@anterior",
                    h.StatusAnterior.HasValue ? (object)StatusToDb(h.StatusAnterior.Value) : DBNull.Value);
                cmd.Parameters.AddWithValue("@novo", StatusToDb(h.StatusNovo));
                cmd.Parameters.AddWithValue("@data", h.DataHora);
                cmd.Parameters.AddWithValue("@usuario", h.Usuario);

                cmd.ExecuteNonQuery();
            }
        }

        public List<HistoricoStatus> ListarPorOS(long osId, NpgsqlConnection conn, NpgsqlTransaction tx = null)
        {
            const string sql = @"
                SELECT id, ordem_servico_id, status_anterior, status_novo, data_hora, usuario
                  FROM historico_status_os
                 WHERE ordem_servico_id = @os_id
                 ORDER BY data_hora DESC;";

            var lista = new List<HistoricoStatus>();
            using (var cmd = new NpgsqlCommand(sql, conn, tx))
            {
                cmd.Parameters.AddWithValue("@os_id", osId);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new HistoricoStatus
                        {
                            Id = reader.GetInt64(reader.GetOrdinal("id")),
                            OrdemServicoId = reader.GetInt64(reader.GetOrdinal("ordem_servico_id")),
                            StatusAnterior = reader.IsDBNull(reader.GetOrdinal("status_anterior"))
                                ? (StatusOS?)null
                                : StatusFromDb(reader.GetString(reader.GetOrdinal("status_anterior"))),
                            StatusNovo = StatusFromDb(reader.GetString(reader.GetOrdinal("status_novo"))),
                            DataHora = reader.GetDateTime(reader.GetOrdinal("data_hora")),
                            Usuario = reader.GetString(reader.GetOrdinal("usuario"))
                        });
                    }
                }
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

        private static StatusOS StatusFromDb(string s)
        {
            switch (s)
            {
                case "ABERTA": return StatusOS.Aberta;
                case "EM_ANDAMENTO": return StatusOS.EmAndamento;
                case "CONCLUIDA": return StatusOS.Concluida;
                case "CANCELADA": return StatusOS.Cancelada;
                default: throw new ArgumentException("Status desconhecido: " + s);
            }
        }
    }
}