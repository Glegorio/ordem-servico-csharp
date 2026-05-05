using System.Collections.Generic;

namespace OrdemServico.Repositories.Common
{
    /// <summary>
    /// Resultado paginado: itens da página atual + total geral
    /// Usado nas listagens 
    /// </summary>
    public class PagedResult<T>
    {
        public List<T> Items { get; set; }
        public int TotalRegistros { get; set; }
        public int Pagina { get; set; }
        public int TamanhoPagina { get; set; }

        public int TotalPaginas
        {
            get
            {
                if (TamanhoPagina == 0) return 0;
                return (int)System.Math.Ceiling((double)TotalRegistros / TamanhoPagina);
            }
        }

        public PagedResult()
        {
            Items = new List<T>();
        }
    }
}