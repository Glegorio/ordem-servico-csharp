using System;
using OrdemServico.Entities.Enums;

namespace OrdemServico.Repositories.Common
{
    /// <summary>
    /// Filtros combináveis para busca de OS
    /// Usado na grid principal e no relatório
    /// </summary>
    public class OrdemServicoFiltro
    {
        public long? ClienteId { get; set; }
        public StatusOS? Status { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
    }
}