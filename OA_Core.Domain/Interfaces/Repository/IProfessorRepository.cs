using OA_Core.Domain.Entities;

namespace OA_Core.Domain.Interfaces.Repository
{
    public interface IProfessorRepository
    {
        Task<Professor> FindAsync(Guid id);
        Task<IEnumerable<Professor>> ListAsync();
        Task<IEnumerable<Professor>> ListPaginationAsync(int page, int rows);
        Task AddAsync(Professor professor);
        Task RemoveAsync(Professor professor);
        Task EditAsync(Professor professor);
    }
}
