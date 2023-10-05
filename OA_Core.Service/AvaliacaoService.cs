using AutoMapper;
using OA_Core.Domain.Contracts.Request;
using OA_Core.Domain.Contracts.Response;
using OA_Core.Domain.Entities;
using OA_Core.Domain.Enums;
using OA_Core.Domain.Exceptions;
using OA_Core.Domain.Interfaces.Notifications;
using OA_Core.Domain.Interfaces.Repository;
using OA_Core.Domain.Interfaces.Service;

namespace OA_Core.Service
{
	public class AvaliacaoService : IAvaliacaoService
	{
		private readonly IAulaRepository _aulaRepository;
		private readonly IAvaliacaoRepository _repository;
		private  readonly IUsuarioRepository _usuarioRepository;
		private readonly IAvaliacaoUsuarioRepository _avaliacaoUsuarioRepository;
		private readonly INotificador _notificador;
		private readonly IMapper _mapper;

		public AvaliacaoService(IAvaliacaoRepository repository, IUsuarioRepository usuarioRepository, INotificador notificador, IMapper mapper, IAulaRepository aulaRepository, IAvaliacaoUsuarioRepository avaliacaoUsuarioRepository)
		{
			_usuarioRepository = usuarioRepository;
			_avaliacaoUsuarioRepository = avaliacaoUsuarioRepository;
			_repository = repository;
			_notificador = notificador;
			_mapper = mapper;
			_aulaRepository = aulaRepository;			
		}

		public async Task<Guid> CadastrarAvaliacaoAsync(AvaliacaoRequest avaliacaoRequest)
		{
			var entity = _mapper.Map<Avaliacao>(avaliacaoRequest);
			if (await _aulaRepository.ObterPorIdAsync(avaliacaoRequest.AulaId) is null)
				throw new InformacaoException(StatusException.NaoEncontrado, $"AulaId {avaliacaoRequest.AulaId} inválida ou não existente");
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
		//A avaliação só pode ser deletada se não tiver nenhum relacionamento com AvaliacaoUsuario.
		//Não é softdelete, nesse caso é delete mesmo.
		//Caso já exista o relacionamento, ela só pode ser desativada.
		public Task DeletarAvaliacaoAsync(Guid id)
		{
			throw new NotImplementedException();
		}

		//A alteração também é permitida se não existir nenhum relacionamento com AvaliacaoUsuario,
		//depois que existe o relacionamento não pode mais.
		public Task EditarAvaliacaoAsync(Guid id, AvaliacaoRequest avaliacaoRequest)
		{
			throw new NotImplementedException();
		}

		//O campo Fim deve ser preenchido ao encerrar a avaliação, com a data e hora atual.
		public async Task EncerrarAvaliacaoAsync(AvaliacaoUsuarioRequest avaliacaoUsuarioRequest)
		{
			var entity = await _avaliacaoUsuarioRepository.ObterAsync(a => a.AvaliacaoId == avaliacaoUsuarioRequest.AvaliacaoId && a.UsuarioId == avaliacaoUsuarioRequest.UsuarioId);
			if (entity is null)
				throw new InformacaoException(StatusException.NaoEncontrado, $"Avaliacao inválida ou não existente");			

			entity.Fim = DateTime.Now;
			await _avaliacaoUsuarioRepository.EditarAsync(entity);
		}

		public async Task IniciarAvaliacaoAsync(AvaliacaoUsuarioRequest avaliacaoUsuarioRequest)
		{
			var entity = _mapper.Map<AvaliacaoUsuario>(avaliacaoUsuarioRequest);			
			if (await _repository.ObterPorIdAsync(avaliacaoUsuarioRequest.AvaliacaoId) is null)
				throw new InformacaoException(StatusException.NaoEncontrado, $"Avaliacao {avaliacaoUsuarioRequest.AvaliacaoId} inválida ou não existente");
			
			if (await _usuarioRepository.ObterPorIdAsync(avaliacaoUsuarioRequest.UsuarioId) is null)
				throw new InformacaoException(StatusException.NaoEncontrado, $"Usuario {avaliacaoUsuarioRequest.UsuarioId} inválido ou não existente");
			
			if (!entity.Valid)
			{
				_notificador.Handle(entity.ValidationResult);
				 throw new InformacaoException(StatusException.FormatoIncorreto, $"AvaliacaoUsuario {avaliacaoUsuarioRequest.AvaliacaoId}{avaliacaoUsuarioRequest.UsuarioId} inválida");

			}
			entity.Inicio = DateTime.Now;
			await _avaliacaoUsuarioRepository.AdicionarAsync(entity);
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
