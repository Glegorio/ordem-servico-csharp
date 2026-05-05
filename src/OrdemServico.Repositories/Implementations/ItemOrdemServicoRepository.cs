using System;
using System.Collections.Generic;
using Npgsql;
using OrdemServico.Entities;

namespace OrdemServico.Repositories.Implementations
{
    public class ItemOrdemServicoRepository
    {
        public long Inserir(ItemOrdemServico item, NpgsqlConnection conn, NpgsqlTransaction tx)
        {
            const string sql = @"
                INSERT INTO itens_ordem_servico
                    (ordem_servico_id, servico_id, quantidade,
                     valor_unitario, percentual_imposto_aplicado, valor_total_item)
                VALUES
                    (@os_id, @servico_id, @quantidade,
                     @valor_unitario, @percentual, @valor_total)
                RETURNING id;";

            using (var cmd = new NpgsqlCommand(sql, conn, tx))
            {
                cmd.Parameters.AddWithValue("@os_id", item.OrdemServicoId);
                cmd.Parameters.AddWithValue("@servico_id", item.ServicoId);
                cmd.Parameters.AddWithValue("@quantidade", item.Quantidade);
                cmd.Parameters.AddWithValue("@valor_unitario", item.ValorUnitario);
                cmd.Parameters.AddWithValue("@percentual", item.PercentualImpostoAplicado);
                cmd.Parameters.AddWithValue("@valor_total", item.ValorTotalItem);

                return Convert.ToInt64(cmd.ExecuteScalar());
            }
        }

        public void Excluir(long itemId, NpgsqlConnection conn, NpgsqlTransaction tx)
        {
            const string sql = "DELETE FROM itens_ordem_servico WHERE id = @id;";
            using (var cmd = new NpgsqlCommand(sql, conn, tx))
            {
                cmd.Parameters.AddWithValue("@id", itemId);
                cmd.ExecuteNonQuery();
            }
        }

        public void ExcluirPorOS(long osId, NpgsqlConnection conn, NpgsqlTransaction tx)
        {
            const string sql = "DELETE FROM itens_ordem_servico WHERE ordem_servico_id = @os_id;";
            using (var cmd = new NpgsqlCommand(sql, conn, tx))
            {
                cmd.Parameters.AddWithValue("@os_id", osId);
                cmd.ExecuteNonQuery();
            }
        }

        public List<ItemOrdemServico> ListarPorOS(long osId, NpgsqlConnection conn, NpgsqlTransaction tx = null)
        {
            const string sql = @"
                SELECT i.id, i.ordem_servico_id, i.servico_id, i.quantidade,
                       i.valor_unitario, i.percentual_imposto_aplicado, i.valor_total_item,
                       s.nome AS servico_nome
                  FROM itens_ordem_servico i
                  JOIN servicos s ON s.id = i.servico_id
                 WHERE i.ordem_servico_id = @os_id
                 ORDER BY i.id;";

            var lista = new List<ItemOrdemServico>();
            using (var cmd = new NpgsqlCommand(sql, conn, tx))
            {
                cmd.Parameters.AddWithValue("@os_id", osId);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new ItemOrdemServico
                        {
                            Id = reader.GetInt64(reader.GetOrdinal("id")),
                            OrdemServicoId = reader.GetInt64(reader.GetOrdinal("ordem_servico_id")),
                            ServicoId = reader.GetInt64(reader.GetOrdinal("servico_id")),
                            Quantidade = reader.GetDecimal(reader.GetOrdinal("quantidade")),
                            ValorUnitario = reader.GetDecimal(reader.GetOrdinal("valor_unitario")),
                            PercentualImpostoAplicado = reader.GetDecimal(reader.GetOrdinal("percentual_imposto_aplicado")),
                            ValorTotalItem = reader.GetDecimal(reader.GetOrdinal("valor_total_item")),
                            ServicoNome = reader.GetString(reader.GetOrdinal("servico_nome"))
                        });
                    }
                }
            }
            return lista;
        }
    }
}