using OA_Core.Domain.Contracts.Request;
using OA_Core.Domain.Contracts.Response;

namespace OA_Core.Domain.Interfaces.Service
{
    public interface IAlunoService
    {
        Task<Guid> PostAlunoAsync(AlunoRequest alunoRequest);
        Task PutAlunoAsync(Guid id, AlunoRequestPut alunoRequest);
        Task DeleteAlunoAsync(Guid id);
        Task<AlunoResponse> GetAlunoByIdAsync(Guid id);
        Task<IEnumerable<AlunoResponse>> GetAllAlunosAsync(int page, int rows);
    }
}
