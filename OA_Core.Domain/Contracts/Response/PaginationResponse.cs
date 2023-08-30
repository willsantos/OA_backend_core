
namespace OA_Core.Domain.Contracts.Response
{
    public class PaginationResponse<T>
    {
        public PaginationResponse(int pagina, int linhasPorPagina, IEnumerable<T> resultado)
        {
            Pagina = pagina;
            LinhasPorPagina = linhasPorPagina;
            Resultado = resultado;
        }

        public int Pagina { get; set; }
        public int LinhasPorPagina { get; set; }

        public IEnumerable<T> Resultado { get; set; }

    }
}
