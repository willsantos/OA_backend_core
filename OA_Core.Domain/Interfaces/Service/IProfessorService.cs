using OA_Core.Domain.Contracts.Request;
using OA_Core.Domain.Contracts.Response;

namespace OA_Core.Domain.Interfaces.Service
{
    public interface IProfessorService
    {
        Task<Guid> PostProfessorAsync(ProfessorRequest professorRequest);
        Task PutProfessorAsync(Guid id, ProfessorRequest professorRequest);
        Task DeleteProfessorAsync(Guid id);
        Task<ProfessorResponse> GetProfessorByIdAsync(Guid id);
        Task<IEnumerable<ProfessorResponse>> GetAllProfessoresAsync(int page, int rows);
    }
}
