using System;
using System.Collections.Generic;
using OrdemServico.Entities.Enums;

namespace OrdemServico.Entities
{
    /// <summary>
    /// Cabeçalho de uma Ordem de Serviço
    /// Espelha a tabela 'ordens_servico' do banco
    /// </summary>
    public class OrdemServico
    {
        public long Id { get; set; }
        public long ClienteId { get; set; }
        public DateTime DataAbertura { get; set; }
        public DateTime? DataConclusao { get; set; }
        public StatusOS Status { get; set; }
        public string Observacao { get; set; }
        public decimal ValorTotal { get; set; }
        public int Versao { get; set; }

        public List<ItemOrdemServico> Itens { get; set; }

        public string ClienteNome { get; set; }

        public OrdemServico()
        {
            Itens = new List<ItemOrdemServico>();
        }
    }
}