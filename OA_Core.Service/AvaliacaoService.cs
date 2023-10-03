using AutoMapper;
using OA_Core.Domain.Contracts.Request;
using OA_Core.Domain.Contracts.Response;
using OA_Core.Domain.Entities;
using OA_Core.Domain.Interfaces.Notifications;
using OA_Core.Domain.Interfaces.Repository;
using OA_Core.Domain.Interfaces.Service;

namespace OA_Core.Service
{
	public class AvaliacaoService : IAvaliacaoService
	{
		private readonly IAvaliacaoRepository _repository;
		private readonly INotificador _notificador;
		private readonly IMapper _mapper;

		public AvaliacaoService(IAvaliacaoRepository repository, INotificador notificador, IMapper mapper)
		{
			_repository = repository;
			_notificador = notificador;
			_mapper = mapper;
		}

		public async Task<Guid> CadastrarAvaliacaoAsync(AvaliacaoRequest avaliacaoRequest)
		{
			var entity = _mapper.Map<Avaliacao>(avaliacaoRequest);
			if (!entity.Valid)
			{
				_notificador.Handle(entity.ValidationResult);
				return Guid.Empty;

			}

			await _repository.AdicionarAsync(entity);
			return entity.Id;
		}
		public Task AtvivarDesativarAvaliacaoAsync(Guid id)
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
