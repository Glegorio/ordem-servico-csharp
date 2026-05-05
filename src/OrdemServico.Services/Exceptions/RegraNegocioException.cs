using System;

namespace OrdemServico.Services.Exceptions
{
    /// <summary>
    /// Exceção lançada quando uma regra de negócio é violada
    /// A UI captura e exibe a mensagem como amigável (sem stack trace)
    /// </summary>
    public class RegraNegocioException : Exception
    {
        public RegraNegocioException(string mensagem) : base(mensagem) { }
    }
}