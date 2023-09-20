namespace OA_Core.Domain.Interfaces.Service
{
	public interface IBaseService<TResponse> where TResponse : class
	{
		Task<TResponse> ObterPorIdAsync<TRequest>(Guid id);
		Task<TResponse> ObterAsync<TRequest>(int page, int rows);
		Task<IEnumerable<TResponse>> ObterTodosAsync(string parametros);
		Task <Guid>AdicionarAsync<TRequest>(TRequest item);
		Task RemoverAsync<TRequest>(Guid id);
		Task EditarAsync<TRequest>(Guid id, TRequest item);
	}
}
