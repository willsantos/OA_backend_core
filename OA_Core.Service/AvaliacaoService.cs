using AutoMapper;
using OA_Core.Domain.Contracts.Request;
using OA_Core.Domain.Contracts.Response;
using OA_Core.Domain.Entities;
using OA_Core.Domain.Enums;
using OA_Core.Domain.Exceptions;
using OA_Core.Domain.Interfaces.Notifications;
using OA_Core.Domain.Interfaces.Repository;
using OA_Core.Domain.Interfaces.Service;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography.X509Certificates;

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

		public async Task AtivivarDesativarAvaliacaoAsync(Guid id, bool ativa)
		{
			var entity = await _repository.ObterPorIdAsync(id);
			if (entity is null)
				throw new InformacaoException(StatusException.NaoEncontrado, $"Avaliacao inválida ou não existente");

			entity.Ativa = ativa;			
			await _repository.EditarAsync(entity);
		}
		
		public async Task DeletarAvaliacaoAsync(Guid id)
		{
			var entity = await _repository.ObterPorIdAsync(id);
			if (entity is null)
				throw new InformacaoException(StatusException.FormatoIncorreto, $"Essa avaliacao não existe ou formato está incorreto");
			if (await _avaliacaoUsuarioRepository.ObterAsync(a => a.AvaliacaoId == id) is not null)
			{
				entity.DataDelecao = DateTime.Now;
				await _repository.EditarAsync(entity);
			}
			else
			await _repository.RemoverAsync(entity);
		}

		public async Task<AvaliacaoResponse> EditarAvaliacaoAsync(Guid id, AvaliacaoRequest avaliacaoRequest)
		{
			var entity = await _repository.ObterPorIdAsync(id);
			var entidadeMapeada = _mapper.Map<Avaliacao>(avaliacaoRequest);
			if(entity is null)
				throw new InformacaoException(StatusException.NaoEncontrado, $"Avaliacao inválida ou não existente");
			if (await _avaliacaoUsuarioRepository.ObterAsync(a => a.AvaliacaoId == entity.Id) is not null)
				throw new InformacaoException(StatusException.Conflito, $"Essa avaliacao nao pode ser editada");

			if (!entity.Valid)
			{
				_notificador.Handle(entity.ValidationResult);
				throw new InformacaoException(StatusException.FormatoIncorreto, $"AvaliacaoUsuario inválida");

			}
			entity.Ativa = entidadeMapeada.Ativa;
			entity.Nome = entidadeMapeada.Nome;
			entity.Tipo = entidadeMapeada.Tipo;
			entity.NotaMaxima = entidadeMapeada.NotaMaxima;
			entity.NotaMinima = entidadeMapeada.NotaMinima;
			entity.TotalQuestoes = entidadeMapeada.TotalQuestoes;
			entity.DataEntrega = entidadeMapeada.DataEntrega;
			entity.AulaId = entidadeMapeada.AulaId;
			entity.DataAlteracao = DateTime.Now;
		
			await _repository.EditarAsync(entity);
			return _mapper.Map<AvaliacaoResponse>(entity);
		}
		
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

		public async Task<AvaliacaoResponse> ObterAvaliacaoPorIdAsync(Guid id)
		{
			var entity = await _repository.ObterPorIdAsync(id) ?? 
				throw new InformacaoException(StatusException.NaoEncontrado, $"Avaliacao {id} inválido ou não existente");		
			
			return _mapper.Map<AvaliacaoResponse>(entity); 
		}

		public Task<IEnumerable<AvaliacaoResponse>> ObterTodasAvaliacoesAsync(int page, int rows)
		{
			throw new NotImplementedException();
		}
	}
}
