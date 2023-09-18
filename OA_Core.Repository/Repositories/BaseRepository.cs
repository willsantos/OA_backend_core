using Microsoft.EntityFrameworkCore;
using OA_Core.Domain.Interfaces.Repository;
using OA_Core.Repository.Context;
using System.Linq.Expressions;

namespace OA_Core.Repository.Repositories
{
	public class BaseRepository<T> : IBaseRepository<T> where T : class
	{
		private readonly CoreDbContext _coreDbContext;

		public BaseRepository(CoreDbContext coreDbContext)
		{
			_coreDbContext = coreDbContext;
		}

		public async Task AdicionarAsync(T item)
		{
			await _coreDbContext.Set<T>().AddAsync(item);
			await _coreDbContext.SaveChangesAsync();
		}

		public async Task EditarAsync(T item)
		{
			_coreDbContext.Set<T>().Update(item);
			await _coreDbContext.SaveChangesAsync();
		}

		public async Task<T> ObterAsync(Expression<Func<T, bool>> expression)
		{
			return await _coreDbContext.Set<T>().AsNoTracking().FirstOrDefaultAsync(expression);
		}

		public async Task<T> ObterPorIdAsync(Guid id)
		{
			return await _coreDbContext.Set<T>().FindAsync(id);
		}

		public async Task<IEnumerable<T>> ObterTodosAsync(Expression<Func<T, bool>> expression)
		{
			return await _coreDbContext.Set<T>().Where(expression).ToListAsync();
		}

		public async Task RemoverAsync(T item)
		{
			_coreDbContext.Set<T>().Remove(item);
			await _coreDbContext.SaveChangesAsync();
		}
	}
}
