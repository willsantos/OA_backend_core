namespace OA_Core.Domain.Interfaces.Service
{
	public interface IBaseService<TResponse, TRequest, T> where TResponse : class where TRequest : class where T : class
	{
		Task<TResponse> ObterPorIdAsync(Guid id);
		Task<TResponse> ObterAsync(int page, int rows);
		Task<IEnumerable<TResponse>> ObterTodosAsync(string parametros);
		Task <Guid>AdicionarAsync(TRequest item);
		Task RemoverAsync(Guid id);
		Task EditarAsync<TRequestPut>(Guid id, TRequest item);
	}
}
