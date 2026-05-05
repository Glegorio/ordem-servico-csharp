using System;
using OrdemServico.Entities.Enums;

namespace OrdemServico.Entities
{
    /// <summary>
    /// Registro de mudança de status de uma OS
    /// Espelha a tabela 'historico_status_os' do banco
    /// </summary>
    public class HistoricoStatus
    {
        public long Id { get; set; }
        public long OrdemServicoId { get; set; }
        public StatusOS? StatusAnterior { get; set; }
        public StatusOS StatusNovo { get; set; }
        public DateTime DataHora { get; set; }
        public string Usuario { get; set; }
    }
}