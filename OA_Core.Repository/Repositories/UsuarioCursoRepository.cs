using Microsoft.EntityFrameworkCore;
using OA_Core.Domain.Entities;
using OA_Core.Domain.Interfaces.Repository;
using OA_Core.Repository.Context;
using MySqlConnector;
using System.Linq.Expressions;

namespace OA_Core.Repository.Repositories
{
	public class UsuarioCursoRepository : BaseRepository<UsuarioCurso>, IUsuarioCursoRepository
	{
		private readonly CoreDbContext _context;

		public UsuarioCursoRepository(CoreDbContext context) : base(context)
		{
			_context = context;
		}

		public async Task<IEnumerable<UsuarioCurso>> ObterTodosComIncludeAsync(Expression<Func<UsuarioCurso, bool>> expression)
		{
			return await _context.Set<UsuarioCurso>()
				.Include(cp => cp.Curso)
				.Where(expression)
				.ToListAsync();
		}
	}
}
