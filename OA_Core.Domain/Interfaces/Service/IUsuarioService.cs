using OA_Core.Domain.Contracts.Request;
using OA_Core.Domain.Contracts.Response;

namespace OA_Core.Domain.Interfaces.Service
{
    public interface IUsuarioService
    {
        Task<Guid> PostUsuarioAsync(UsuarioRequest usuarioRequest);
        Task PutUsuarioAsync(Guid id, UsuarioRequest usuarioRequest);
        Task DeleteUsuarioAsync(Guid id);
        Task<UsuarioResponse> GetUsuarioByIdAsync(Guid id);
        Task<IEnumerable<UsuarioResponse>> GetAllUsuariosAsync(int page, int rows);
    }
}
