using OA_Core.Domain.Contracts.Response;
using OA_Core.Domain.Entities;
using System.Linq.Expressions;

namespace OA_Core.Domain.Interfaces.Repository
{
    public interface ICursoProfessorRepository : IBaseRepository<CursoProfessor>
    {
		Task<IEnumerable<CursoProfessor>> ObterTodosComIncludeAsync(Expression<Func<CursoProfessor, bool>> expression);
	}
}
