namespace OrdemServico.Entities.Enums
{
    /// <summary>
    /// Status de uma Ordem de Serviço
    /// 'status_os_enum' do banco
    /// </summary>
    public enum StatusOS
    {
        Aberta = 1,
        EmAndamento = 2,
        Concluida = 3,
        Cancelada = 4
    }
}