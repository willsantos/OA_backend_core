using System.Linq.Expressions;

namespace OA_Core.Domain.Interfaces.Service
{
	public interface IBaseService<TResponse> where TResponse : class
	{
		Task<TResponse> ObterPorIdAsync(Guid id);
		Task<IEnumerable<TResponse>> ObterTodosAsync(int page, int rows);
		Task<TResponse> ObterAsync(string parametros);
		Task<IEnumerable<TResponse>> ObterTodosAsync(string expression);
		Task <Guid>AdicionarAsync<TRequest>(TRequest item);
		Task RemoverAsync<TRequest>(Guid id);
		Task EditarAsync<TRequestPut>(Guid id, TRequestPut item);
	}
}
