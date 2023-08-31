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
        Task<Guid> PostUsuarioAsync(AlunoRequest alunoRequest);
        Task PutUsuarioAsync(Guid id, AlunoRequest alunoRequest);
        Task DeleteUsuarioAsync(Guid id);
        Task<AlunoResponse> GetUsuarioByIdAsync(Guid id);
        Task<IEnumerable<AlunoResponse>> GetAllUsuariosAsync(int page, int rows);
    }
}
