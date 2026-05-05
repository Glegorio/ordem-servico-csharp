using System;

namespace OrdemServico.Services.Exceptions
{
    /// <summary>
    /// Lançada quando o controle de concorrência otimista detecta
    /// que outro usuário alterou o registro entre a leitura e a gravação
    /// </summary>
    public class ConcorrenciaException : Exception
    {
        public ConcorrenciaException(string mensagem) : base(mensagem) { }
    }
}