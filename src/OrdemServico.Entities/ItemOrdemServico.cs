namespace OrdemServico.Entities
{
    /// <summary>
    /// Item (linha) de uma Ordem de Serviço
    /// Espelha a tabela 'itens_ordem_servico' do banco
    /// ValorUnitario e PercentualImpostoAplicado são
    /// Cópias congeladas do estado do serviço no momento da criação
    /// </summary>
    /// 
    public class ItemOrdemServico
    {
        public long Id { get; set; }
        public long OrdemServicoId { get; set; }
        public long ServicoId { get; set; }
        public decimal Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal PercentualImpostoAplicado { get; set; }
        public decimal ValorTotalItem { get; set; }
        
        /// <summary>
        /// Nome do serviço (para exibição em telas e relatórios)
        /// Não é uma coluna da tabela — é populada via JOIN nos repositórios
        /// </summary>
        public string ServicoNome { get; set; }
    }
}