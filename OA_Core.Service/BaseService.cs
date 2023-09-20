using OA_Core.Domain.Interfaces.Service;

namespace OA_Core.Service
{
	public class BaseService<T> : IBaseService<T> where T : class 
	{
		public Task<Guid> AdicionarAsync<TRequest>(TRequest item)
		{
			throw new NotImplementedException();
		}

		public Task EditarAsync<TRequest>(Guid id, TRequest item)
		{
			throw new NotImplementedException();
		}

		public Task<T> ObterAsync<TRequest>(int page, int rows)
		{
			throw new NotImplementedException();
		}

		public Task<T> ObterPorIdAsync<TRequest>(Guid id)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<T>> ObterTodosAsync(string parametros)
		{
			throw new NotImplementedException();
		}

		public Task RemoverAsync<TRequest>(Guid id)
		{
			throw new NotImplementedException();
		}
	}
}
