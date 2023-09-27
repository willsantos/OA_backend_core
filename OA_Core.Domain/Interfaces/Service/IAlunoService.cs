using OA_Core.Domain.Contracts.Request;
using OA_Core.Domain.Contracts.Response;

namespace OA_Core.Domain.Interfaces.Service
{
    public interface IAlunoService
    {
        Task<Guid> CadastrarAlunoAsync(AlunoRequest alunoRequest);
        Task EditarAlunoAsync(Guid id, AlunoRequestPut alunoRequest);
        Task DeletarAlunoAsync(Guid id);
        Task<AlunoResponse> ObterAlunoPorIdAsync(Guid id);
        Task<IEnumerable<AlunoResponse>> ObterTodosAlunosAsync(int page, int rows);
    }
}
