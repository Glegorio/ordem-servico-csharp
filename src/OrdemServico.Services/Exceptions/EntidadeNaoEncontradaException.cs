using System;

namespace OrdemServico.Services.Exceptions
{
    public class EntidadeNaoEncontradaException : Exception
    {
        public EntidadeNaoEncontradaException(string mensagem) : base(mensagem) { }
    }
}