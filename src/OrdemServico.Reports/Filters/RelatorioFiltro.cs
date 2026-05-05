using System;
using OrdemServico.Entities.Enums;

namespace OrdemServico.Reports.Filters
{
    /// <summary>
    /// Filtros do relatório gerencial:
    /// período, cliente e status — todos opcionais e combináveis.
    /// </summary>
    public class RelatorioFiltro
    {
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public long? ClienteId { get; set; }
        public StatusOS? Status { get; set; }
    }
}