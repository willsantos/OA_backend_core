using OA_Core.Domain.Contracts.Request;
using OA_Core.Domain.Contracts.Response;

namespace OA_Core.Domain.Interfaces.Service
{
    public interface IProfessorService
    {
        Task<Guid> CadastrarProfessorAsync(ProfessorRequest professorRequest);
        Task EditarProfessorAsync(Guid id, ProfessorRequestPut professorRequest);
        Task DeletarProfessorAsync(Guid id);
        Task<ProfessorResponse> ObterProfessorPorIdAsync(Guid id);
        Task<IEnumerable<ProfessorResponse>> ObterTodosProfessoresAsync(int page, int rows);
    }
}
