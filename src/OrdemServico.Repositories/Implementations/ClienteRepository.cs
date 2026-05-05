using System;
using System.Collections.Generic;
using System.Text;
using Npgsql;
using NpgsqlTypes;
using OrdemServico.Entities;
using OrdemServico.Entities.Enums;
using OrdemServico.Repositories.Common;

namespace OrdemServico.Repositories.Implementations
{
    /// <summary>
    /// Acesso a dados de Cliente
    /// Aceita NpgsqlConnection e NpgsqlTransaction opcionais para
    /// permitir participação em transações da camada Service
    /// </summary>
    public class ClienteRepository
    {
        public long Inserir(Cliente cliente, NpgsqlConnection conn, NpgsqlTransaction tx = null)
        {
            const string sql = @"
                INSERT INTO clientes (nome, documento, tipo, email, telefone, ativo)
                VALUES (@nome, @documento, @tipo::tipo_cliente_enum, @email, @telefone, @ativo)
                RETURNING id;";

            using (var cmd = new NpgsqlCommand(sql, conn, tx))
            {
                cmd.Parameters.AddWithValue("@nome", cliente.Nome);
                cmd.Parameters.AddWithValue("@documento", cliente.Documento);
                cmd.Parameters.AddWithValue("@tipo", cliente.Tipo.ToString().ToUpper());
                cmd.Parameters.AddWithValue("@email", (object)cliente.Email ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@telefone", (object)cliente.Telefone ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@ativo", cliente.Ativo);

                return Convert.ToInt64(cmd.ExecuteScalar());
            }
        }

        public void Atualizar(Cliente cliente, NpgsqlConnection conn, NpgsqlTransaction tx = null)
        {
            const string sql = @"
                UPDATE clientes
                   SET nome = @nome,
                       documento = @documento,
                       tipo = @tipo::tipo_cliente_enum,
                       email = @email,
                       telefone = @telefone,
                       ativo = @ativo
                 WHERE id = @id;";

            using (var cmd = new NpgsqlCommand(sql, conn, tx))
            {
                cmd.Parameters.AddWithValue("@id", cliente.Id);
                cmd.Parameters.AddWithValue("@nome", cliente.Nome);
                cmd.Parameters.AddWithValue("@documento", cliente.Documento);
                cmd.Parameters.AddWithValue("@tipo", cliente.Tipo.ToString().ToUpper());
                cmd.Parameters.AddWithValue("@email", (object)cliente.Email ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@telefone", (object)cliente.Telefone ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@ativo", cliente.Ativo);

                cmd.ExecuteNonQuery();
            }
        }

        public void Excluir(long id, NpgsqlConnection conn, NpgsqlTransaction tx = null)
        {
            const string sql = "DELETE FROM clientes WHERE id = @id;";

            using (var cmd = new NpgsqlCommand(sql, conn, tx))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public Cliente ObterPorId(long id, NpgsqlConnection conn, NpgsqlTransaction tx = null)
        {
            const string sql = @"
                SELECT id, nome, documento, tipo, email, telefone, data_cadastro, ativo
                  FROM clientes
                 WHERE id = @id;";

            using (var cmd = new NpgsqlCommand(sql, conn, tx))
            {
                cmd.Parameters.AddWithValue("@id", id);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                        return MapearCliente(reader);
                }
            }
            return null;
        }

        public int ContarOSVinculadas(long clienteId, NpgsqlConnection conn, NpgsqlTransaction tx = null)
        {
            const string sql = "SELECT COUNT(*) FROM ordens_servico WHERE cliente_id = @id;";

            using (var cmd = new NpgsqlCommand(sql, conn, tx))
            {
                cmd.Parameters.AddWithValue("@id", clienteId);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public PagedResult<Cliente> Buscar(ClienteFiltro filtro, int pagina, int tamanhoPagina,
                                            NpgsqlConnection conn, NpgsqlTransaction tx = null)
        {
            var where = new StringBuilder(" WHERE 1=1 ");
            var parametros = new List<NpgsqlParameter>();

            if (!string.IsNullOrWhiteSpace(filtro.Nome))
            {
                where.Append(" AND nome ILIKE @nome ");
                parametros.Add(new NpgsqlParameter("@nome", "%" + filtro.Nome + "%"));
            }

            if (!string.IsNullOrWhiteSpace(filtro.Documento))
            {
                where.Append(" AND documento = @documento ");
                parametros.Add(new NpgsqlParameter("@documento", filtro.Documento));
            }

            if (filtro.Ativo.HasValue)
            {
                where.Append(" AND ativo = @ativo ");
                parametros.Add(new NpgsqlParameter("@ativo", filtro.Ativo.Value));
            }

            // Conta total
            string sqlCount = "SELECT COUNT(*) FROM clientes" + where.ToString();
            int total;
            using (var cmd = new NpgsqlCommand(sqlCount, conn, tx))
            {
                foreach (var p in parametros) cmd.Parameters.Add(p.Clone());
                total = Convert.ToInt32(cmd.ExecuteScalar());
            }

            // Busca a página
            string sqlPagina = @"
                SELECT id, nome, documento, tipo, email, telefone, data_cadastro, ativo
                  FROM clientes" + where.ToString() + @"
                 ORDER BY nome
                 LIMIT @limit OFFSET @offset;";

            var resultado = new PagedResult<Cliente>
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
                        resultado.Items.Add(MapearCliente(reader));
                }
            }

            return resultado;
        }

        private Cliente MapearCliente(NpgsqlDataReader reader)
        {
            return new Cliente
            {
                Id = reader.GetInt64(reader.GetOrdinal("id")),
                Nome = reader.GetString(reader.GetOrdinal("nome")),
                Documento = reader.GetString(reader.GetOrdinal("documento")),
                Tipo = (TipoCliente)Enum.Parse(typeof(TipoCliente),
                    ToPascal(reader.GetString(reader.GetOrdinal("tipo")))),
                Email = reader.IsDBNull(reader.GetOrdinal("email")) ? null : reader.GetString(reader.GetOrdinal("email")),
                Telefone = reader.IsDBNull(reader.GetOrdinal("telefone")) ? null : reader.GetString(reader.GetOrdinal("telefone")),
                DataCadastro = reader.GetDateTime(reader.GetOrdinal("data_cadastro")),
                Ativo = reader.GetBoolean(reader.GetOrdinal("ativo"))
            };
        }

        private static string ToPascal(string s)
        {
            if (string.IsNullOrEmpty(s)) return s;
            // 'FISICA' -> 'Fisica', 'JURIDICA' -> 'Juridica'
            return char.ToUpper(s[0]) + s.Substring(1).ToLower();
        }
    }
}