using System;
using OrdemServico.Entities.Enums;

namespace OrdemServico.Entities
{
    /// <summary>
    /// Representa um cliente cadastrado no sistema
    /// Espelha a tabela 'clientes' do banco
    /// </summary>
    public class Cliente
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Documento { get; set; }
        public TipoCliente Tipo { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public DateTime DataCadastro { get; set; }
        public bool Ativo { get; set; }
    }
}