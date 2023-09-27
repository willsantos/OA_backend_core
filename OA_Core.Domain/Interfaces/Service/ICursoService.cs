using OA_Core.Domain.Contracts.Request;
using OA_Core.Domain.Contracts.Response;

namespace OA_Core.Domain.Interfaces.Service
{
    public interface ICursoService
    {
        Task<Guid> CadastrarCursoAsync(CursoRequest cursoRequest);
        Task EditarCursoAsync(Guid id, CursoRequestPut cursoRequest);
        Task DeletarCursoAsync(Guid id);
        Task<CursoResponse> ObterCursoPorIdAsync(Guid id);
        Task<IEnumerable<CursoResponse>> ObterTodosCursosAsync(int page, int rows);
    }
}
