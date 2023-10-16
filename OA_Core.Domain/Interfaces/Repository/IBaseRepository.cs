using System.Linq.Expressions;

namespace OA_Core.Domain.Interfaces.Repository
{
	public interface IBaseRepository<T> where T : class
	{
		Task<T> ObterPorIdAsync(Guid id);
		Task<T> ObterAsync(Expression<Func<T, bool>> expression);
		Task<IEnumerable<T>> ObterTodosAsync(Expression<Func<T, bool>> expression);
		Task<IEnumerable<T>> ObterTodosAsync(int page, int rows);
		Task AdicionarAsync(T item);
		Task RemoverAsync(T item);
		Task EditarAsync(T item);
		Task EditarVariosAsync(IEnumerable<T> itens);
	}
}
