using OA_Core.Domain.Contracts.Response;
using OA_Core.Domain.Entities;
using System.Linq.Expressions;

namespace OA_Core.Domain.Interfaces.Repository
{
    public interface IAulaRepository
    {
        Task<Aula> FindAsync(Guid id);
        Task<IEnumerable<Aula>> ListAsync();
        Task<IEnumerable<Aula>> ListPaginationAsync(int page, int rows);
        Task AddAsync(Aula aula);
        Task RemoveAsync(Aula aula);
        Task EditAsync(Aula aula);
    }
}
