using OA_Core.Domain.Contracts.Request;
using OA_Core.Domain.Contracts.Response;

namespace OA_Core.Domain.Interfaces.Service
{
    public interface IAulaService
    {
        Task<Guid> PostAulaAsync(AulaRequest aulaRequest);
        Task PutAulaAsync(Guid id, AulaRequestPut aulaRequest);
        Task DeleteAulaAsync(Guid id);
        Task<AulaResponse> GetAulaByIdAsync(Guid id);
        Task<IEnumerable<AulaResponse>> GetAllAulasAsync(int page, int rows);
    }
}
