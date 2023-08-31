using OA_Core.Domain.Contracts.Request;
using OA_Core.Domain.Contracts.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_Core.Domain.Interfaces.Service
{
    public interface IAlunoService
    {
        Task<Guid> PostAlunoAsync(AlunoRequest alunoRequest);
        Task PutAlunoAsync(Guid id, AlunoRequest alunoRequest);
        Task DeleteAlunoAsync(Guid id);
        Task<AlunoResponse> GetAlunoByIdAsync(Guid id);
        Task<IEnumerable<AlunoResponse>> GetAllAlunosAsync(int page, int rows);
    }
}
