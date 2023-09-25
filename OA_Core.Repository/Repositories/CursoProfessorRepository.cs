using Microsoft.EntityFrameworkCore;
using OA_Core.Domain.Entities;
using OA_Core.Domain.Interfaces.Repository;
using OA_Core.Repository.Context;
using MySqlConnector;
using System.Linq.Expressions;

namespace OA_Core.Repository.Repositories
{
	public class CursoProfessorRepository : BaseRepository<CursoProfessor>, ICursoProfessorRepository
	{
		private readonly CoreDbContext _context;

		public CursoProfessorRepository(CoreDbContext context) : base(context)
		{
			_context = context;
		}

		public async Task<IEnumerable<CursoProfessor>> ObterTodosComIncludeAsync(Expression<Func<CursoProfessor, bool>> expression)
		{
			return await _context.Set<CursoProfessor>()
				.Include(cp => cp.Professor)
				.Where(expression)
				.ToListAsync();
		}
	}
}
