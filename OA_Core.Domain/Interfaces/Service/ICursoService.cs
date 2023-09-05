using OA_Core.Domain.Contracts.Request;
using OA_Core.Domain.Contracts.Response;

namespace OA_Core.Domain.Interfaces.Service
{
    public interface ICursoService
    {
        Task<Guid> PostCursoAsync(CursoRequest cursoRequest);
        Task PutCursoAsync(Guid id, CursoRequestPut cursoRequest);
        Task DeleteCursoAsync(Guid id);
        Task<CursoResponse> GetCursoByIdAsync(Guid id);
        Task<IEnumerable<CursoResponse>> GetAllCursosAsync(int page, int rows);
    }
}
