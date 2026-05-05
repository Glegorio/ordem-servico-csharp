using System;

namespace OrdemServico.Reports.Dtos
{
    /// <summary>
    /// Projeção achatada de uma OS para uso no relatório gerencial.
    /// O ReportViewer trabalha melhor com objetos achatados que com
    /// hierarquia de entidades. 
    /// </summary>
    public class RelatorioOrdemDto
    {
        public long OrdemId { get; set; }
        public DateTime DataAbertura { get; set; }
        public DateTime? DataConclusao { get; set; }
        public string Status { get; set; }
        public long ClienteId { get; set; }
        public string ClienteNome { get; set; }
        public string ClienteDocumento { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal ValorImposto { get; set; }
    }
}