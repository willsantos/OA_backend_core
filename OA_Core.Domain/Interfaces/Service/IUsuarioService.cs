using OA_Core.Domain.Contracts.Response;
using OA_Core.Domain.Entities;

namespace OA_Core.Domain.Interfaces.Service
{
    public interface IUsuarioService
    {
        Task<Guid> PostUsuarioAsync(Usuario usuario);
        Task PutUsuarioAsync(Guid id, Usuario usuario);
        Task DeleteUsuarioAsync(Guid id);
        Task<Usuario> GetUsuarioByIdAsync(Guid id);
        Task<IEnumerable<Usuario>> GetAllUsuariosAsync(int page, int rows);
    }
}
