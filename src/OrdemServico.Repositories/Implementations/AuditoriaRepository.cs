using System;
using Npgsql;
using NpgsqlTypes;
using OrdemServico.Entities;

namespace OrdemServico.Repositories.Implementations
{
    public class AuditoriaRepository
    {
        public void Inserir(RegistroAuditoria reg, NpgsqlConnection conn, NpgsqlTransaction tx)
        {
            const string sql = @"
                INSERT INTO auditoria
                    (entidade, id_registro, operacao, data_hora, usuario, snapshot)
                VALUES
                    (@entidade, @id_registro, @operacao, @data, @usuario, @snapshot::jsonb);";

            using (var cmd = new NpgsqlCommand(sql, conn, tx))
            {
                cmd.Parameters.AddWithValue("@entidade", reg.Entidade);
                cmd.Parameters.AddWithValue("@id_registro", reg.IdRegistro);
                cmd.Parameters.AddWithValue("@operacao", reg.Operacao);
                cmd.Parameters.AddWithValue("@data", reg.DataHora);
                cmd.Parameters.AddWithValue("@usuario", reg.Usuario);
                cmd.Parameters.AddWithValue("@snapshot", reg.SnapshotJson);

                cmd.ExecuteNonQuery();
            }
        }
    }
}