using OA_Core.Domain.Contracts.Request;
using OA_Core.Domain.Contracts.Response;

namespace OA_Core.Domain.Interfaces.Service
{
    public interface IUsuarioService
    {
        Task<Guid> CadastrarUsuarioAsync(UsuarioRequest usuarioRequest);
        Task EditarUsuarioAsync(Guid id, UsuarioRequest usuarioRequest);
        Task DeletarUsuarioAsync(Guid id);
        Task<UsuarioResponse> ObterUsuarioPorIdAsync(Guid id);
        Task<IEnumerable<UsuarioResponse>> ObterTodosUsuariosAsync(int page, int rows);
    }
}
