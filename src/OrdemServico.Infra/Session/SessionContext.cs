namespace OrdemServico.Infra.Session
{
    /// <summary>
    /// Contexto da sessão atual da aplicação
    /// Guarda o usuário logado para uso na auditoria
    /// </summary>
    public static class SessionContext
    {
        public static string UsuarioAtual { get; set; }

        public static bool EstaAutenticado
        {
            get { return !string.IsNullOrWhiteSpace(UsuarioAtual); }
        }

        public static void Limpar()
        {
            UsuarioAtual = null;
        }
    }
}