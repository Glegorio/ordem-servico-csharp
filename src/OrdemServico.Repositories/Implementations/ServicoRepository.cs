using System;
using System.Collections.Generic;
using Npgsql;
using OrdemServico.Entities;

namespace OrdemServico.Repositories.Implementations
{
    public class ServicoRepository
    {
        public long Inserir(Servico servico, NpgsqlConnection conn, NpgsqlTransaction tx = null)
        {
            const string sql = @"
                INSERT INTO servicos (nome, valor_base, percentual_imposto, ativo)
                VALUES (@nome, @valor_base, @percentual_imposto, @ativo)
                RETURNING id;";

            using (var cmd = new NpgsqlCommand(sql, conn, tx))
            {
                cmd.Parameters.AddWithValue("@nome", servico.Nome);
                cmd.Parameters.AddWithValue("@valor_base", servico.ValorBase);
                cmd.Parameters.AddWithValue("@percentual_imposto", servico.PercentualImposto);
                cmd.Parameters.AddWithValue("@ativo", servico.Ativo);

                return Convert.ToInt64(cmd.ExecuteScalar());
            }
        }

        public void Atualizar(Servico servico, NpgsqlConnection conn, NpgsqlTransaction tx = null)
        {
            const string sql = @"
                UPDATE servicos
                   SET nome = @nome,
                       valor_base = @valor_base,
                       percentual_imposto = @percentual_imposto,
                       ativo = @ativo
                 WHERE id = @id;";

            using (var cmd = new NpgsqlCommand(sql, conn, tx))
            {
                cmd.Parameters.AddWithValue("@id", servico.Id);
                cmd.Parameters.AddWithValue("@nome", servico.Nome);
                cmd.Parameters.AddWithValue("@valor_base", servico.ValorBase);
                cmd.Parameters.AddWithValue("@percentual_imposto", servico.PercentualImposto);
                cmd.Parameters.AddWithValue("@ativo", servico.Ativo);

                cmd.ExecuteNonQuery();
            }
        }

        public Servico ObterPorId(long id, NpgsqlConnection conn, NpgsqlTransaction tx = null)
        {
            const string sql = @"
                SELECT id, nome, valor_base, percentual_imposto, ativo
                  FROM servicos
                 WHERE id = @id;";

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

        public List<Servico> ListarAtivos(NpgsqlConnection conn, NpgsqlTransaction tx = null)
        {
            const string sql = @"
                SELECT id, nome, valor_base, percentual_imposto, ativo
                  FROM servicos
                 WHERE ativo = TRUE
                 ORDER BY nome;";

            var lista = new List<Servico>();
            using (var cmd = new NpgsqlCommand(sql, conn, tx))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read()) lista.Add(Mapear(reader));
            }
            return lista;
        }

        public List<Servico> ListarTodos(NpgsqlConnection conn, NpgsqlTransaction tx = null)
        {
            const string sql = @"
                SELECT id, nome, valor_base, percentual_imposto, ativo
                  FROM servicos
                 ORDER BY nome;";

            var lista = new List<Servico>();
            using (var cmd = new NpgsqlCommand(sql, conn, tx))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read()) lista.Add(Mapear(reader));
            }
            return lista;
        }

        private Servico Mapear(NpgsqlDataReader reader)
        {
            return new Servico
            {
                Id = reader.GetInt64(reader.GetOrdinal("id")),
                Nome = reader.GetString(reader.GetOrdinal("nome")),
                ValorBase = reader.GetDecimal(reader.GetOrdinal("valor_base")),
                PercentualImposto = reader.GetDecimal(reader.GetOrdinal("percentual_imposto")),
                Ativo = reader.GetBoolean(reader.GetOrdinal("ativo"))
            };
        }
    }
}