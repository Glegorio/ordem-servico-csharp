namespace OrdemServico.Entities
{
    /// <summary>
    /// Representa um serviço do catálogo
    /// Espelha a tabela 'servicos' do banco
    /// </summary>
    public class Servico
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public decimal ValorBase { get; set; }
        public decimal PercentualImposto { get; set; }
        public bool Ativo { get; set; }
    }
}