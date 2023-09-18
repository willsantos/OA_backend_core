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
			try
			{
				await _coreDbContext.Set<T>().AddAsync(item);
				await _coreDbContext.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task EditarAsync(T item)
		{
			try
			{
				_coreDbContext.Set<T>().Update(item);
				await _coreDbContext.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<T> ObterAsync(Expression<Func<T, bool>> expression)
		{
			try
			{
				return await _coreDbContext.Set<T>().AsNoTracking().FirstOrDefaultAsync(expression);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<T> ObterPorIdAsync(Guid id)
		{
			try
			{
				return await _coreDbContext.Set<T>().FindAsync(id);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<IEnumerable<T>> ObterTodosAsync(Expression<Func<T, bool>> expression)
		{
			try
			{
				return await _coreDbContext.Set<T>().Where(expression).ToListAsync();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task RemoverAsync(T item)
		{
			try
			{
				_coreDbContext.Set<T>().Remove(item);
				await _coreDbContext.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}
