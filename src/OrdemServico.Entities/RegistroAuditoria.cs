using System;

namespace OrdemServico.Entities
{
    /// <summary>
    /// Registro de auditoria
    /// Espelha a tabela 'auditoria' do banco
    /// 'Snapshot' guarda o estado do registro (JSON)
    /// </summary>
    public class RegistroAuditoria
    {
        public long Id { get; set; }
        public string Entidade { get; set; }
        public long IdRegistro { get; set; }
        public string Operacao { get; set; }
        public DateTime DataHora { get; set; }
        public string Usuario { get; set; }

        public string SnapshotJson { get; set; }
    }
}