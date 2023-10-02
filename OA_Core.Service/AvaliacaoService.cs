using OA_Core.Domain.Contracts.Request;
using OA_Core.Domain.Contracts.Response;
using OA_Core.Domain.Interfaces.Repository;
using OA_Core.Domain.Interfaces.Service;

namespace OA_Core.Service
{
	public class AvaliacaoService : IAvaliacaoService
	{
		private readonly IAvaliacaoRepository _repository;
		public Task AtvivarDesativarAvaliacaoAsync(Guid id)
		{
			throw new NotImplementedException();
		}

		public Task<Guid> CadastrarAvaliacaoAsync(AvaliacaoRequest avaliacaoRequest)
		{
			throw new NotImplementedException();
		}

		public Task DeletarAvaliacaoAsync(Guid id)
		{
			throw new NotImplementedException();
		}

		public Task EditarAvaliacaoAsync(Guid id, AvaliacaoRequest avaliacaoRequest)
		{
			throw new NotImplementedException();
		}

		public Task EncerrarAvaliacaoAsync(Guid id)
		{
			throw new NotImplementedException();
		}

		public Task<Guid> IniciarAvaliacaoAsync(AvaliacaoRequest avaliacaoRequest)
		{
			throw new NotImplementedException();
		}

		public Task<AvaliacaoResponse> ObterAvaliacaoPorIdAsync(Guid id)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<AvaliacaoResponse>> ObterTodasAvaliacoesAsync(int page, int rows)
		{
			throw new NotImplementedException();
		}
	}
}
