using OA_Core.Domain.Contracts.Response;
using OA_Core.Domain.Entities;
using System.Linq.Expressions;

namespace OA_Core.Domain.Interfaces.Repository
{
    public interface IUsuarioRepository
    {
        Task<Usuario> FindAsync(Guid id);
        Task<IEnumerable<Usuario>> ListAsync();
        Task<IEnumerable<Usuario>> ListPaginationAsync(int page, int rows);
        Task AddAsync(Usuario usuario);
        Task RemoveAsync(Usuario usuario);
        Task EditAsync(Usuario usuario);
    }
}
