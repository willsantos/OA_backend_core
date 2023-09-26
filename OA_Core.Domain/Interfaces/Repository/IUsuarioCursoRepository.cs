using OA_Core.Domain.Contracts.Response;
using OA_Core.Domain.Entities;
using System.Linq.Expressions;

namespace OA_Core.Domain.Interfaces.Repository
{
    public interface IUsuarioCursoRepository : IBaseRepository<UsuarioCurso>
    {
		Task<IEnumerable<UsuarioCurso>> ObterTodosComIncludeAsync(Expression<Func<UsuarioCurso, bool>> expression);
	}
}
