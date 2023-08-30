using OA_Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_Core.Domain.Interfaces.Repository
{
    public interface IAlunoRepository
    {
        Task<Aluno> FindAsync(Guid id);
        Task<IEnumerable<Aluno>> ListAsync();
        Task<IEnumerable<Aluno>> ListPaginationAsync(int page, int rows);
        Task AddAsync(Aluno aluno);
        Task RemoveAsync(Aluno aluno);
        Task EditAsync(Aluno aluno);
    }
}
