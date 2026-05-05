namespace OrdemServico.Repositories.Common
{
    /// <summary>
    /// Filtros combináveis para busca de clientes
    /// Qualquer campo pode ser nulo 
    /// </summary>
    public class ClienteFiltro
    {
        public string Nome { get; set; }
        public string Documento { get; set; }
        public bool? Ativo { get; set; }
    }
}