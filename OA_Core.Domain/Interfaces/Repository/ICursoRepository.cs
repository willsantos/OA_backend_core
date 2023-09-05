using OA_Core.Domain.Contracts.Response;
using OA_Core.Domain.Entities;
using System.Linq.Expressions;

namespace OA_Core.Domain.Interfaces.Repository
{
    public interface ICursoRepository
    {
        Task<Curso> FindAsync(Guid id);
        Task<IEnumerable<Curso>> ListAsync();
        Task<IEnumerable<Curso>> ListPaginationAsync(int page, int rows);
        Task AddAsync(Curso curso);
        Task RemoveAsync(Curso curso);
        Task EditAsync(Curso curso);
    }
}
