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
	public class AulaService : IAulaService
	{
		private readonly IMapper _mapper;
		private readonly IAulaRepository _aulaRepository;
		private readonly ICursoRepository _cursoRepository;
		private readonly INotificador _notificador;

		public AulaService(IMapper mapper, IAulaRepository aulaRepository, ICursoRepository cursoRepository, INotificador notificador)
		{
			_mapper = mapper;
			_aulaRepository = aulaRepository;
			_cursoRepository = cursoRepository;
			_notificador = notificador;
		}

		public async Task DeletarAulaAsync(Guid id)
		{
			var aula = await _aulaRepository.ObterPorIdAsync(id) ??
				throw new InformacaoException(StatusException.NaoEncontrado, $"Aula {id} não encontrado");

			aula.DataDelecao = DateTime.Now;
			await _aulaRepository.EditarAsync(aula);
		}

		public async Task<IEnumerable<AulaResponse>> ObterTodasAulasAsync(int page, int rows)
		{
			var listEntity = await _aulaRepository.ObterTodosAsync(page, rows);

			var listResponse = new List<AulaResponse>();
			foreach (var item in listEntity)
			{
				var response = _mapper.Map<AulaResponse>(item);
				listResponse.Add(response);
			}

			return _mapper.Map<IEnumerable<AulaResponse>>(listResponse);
		}

		public async Task<AulaResponse> ObterAulaPorIdAsync(Guid id)
		{
			var aula = await _aulaRepository.ObterPorIdAsync(id) ??
					throw new InformacaoException(StatusException.NaoEncontrado, $"Aula {id} não encontrado");

			return _mapper.Map<AulaResponse>(aula);
		}

		public async Task<Guid> CadastrarAulaAsync(AulaRequest request)
		{
			Aula entity = AulaTPHMapper(request);

			if (await _cursoRepository.ObterPorIdAsync(request.CursoId) is null)
				throw new InformacaoException(StatusException.NaoEncontrado, $"CursoId: {request.CursoId} inválido ou não existente");

			if (!entity.Valid)
			{
				_notificador.Handle(entity.ValidationResult);
				return Guid.Empty;
			}

			await _aulaRepository.AdicionarAsync(entity);
			return entity.Id;
		}

		public async Task EditarAulaAsync(Guid id, AulaRequestPut request)
		{
			var find = await _aulaRepository.ObterAsync(x => x.Id == id) ??
				throw new InformacaoException(StatusException.NaoEncontrado, $"Aula {id} não encontrada");

			if (find.Tipo != request.Tipo)
			{
				throw new InformacaoException(StatusException.Conflito, $"Não é possível mudar o tipo da aula");
			}

			Aula entity = AulaTPHMapper(request);
			entity.Id = id;
			entity.CursoId = find.CursoId;

			if (!entity.Valid)
			{
				_notificador.Handle(entity.ValidationResult);
				return;
			}

			await _aulaRepository.EditarAsync(entity);
		}

		public async Task EditarOrdemAulaAsync(Guid id, OrdemRequest ordem)
		{
			var entity = await _aulaRepository.ObterPorIdAsync(id) ??
							throw new InformacaoException(StatusException.NaoEncontrado, $"Aula {id} não encontrado");

			entity.Ordem = ordem.Ordem;
			entity.DataAlteracao = DateTime.Now;

			await _aulaRepository.EditarAsync(entity);

		}

		private Aula AulaTPHMapper(AulaRequestPut aulaRequest)
		{
			Aula entity;

			switch (aulaRequest.Tipo)
			{
				case TipoAula.AulaOnline:
					entity = _mapper.Map<AulaOnline>(aulaRequest);
					break;
				case TipoAula.AulaVideo:
					entity = _mapper.Map<AulaVideo>(aulaRequest);
					break;
				case TipoAula.AulaTexto:
					entity = _mapper.Map<AulaTexto>(aulaRequest);
					break;
				case TipoAula.AulaDownload:
					entity = _mapper.Map<AulaDownload>(aulaRequest);
					break;
				default:
					throw new InformacaoException(StatusException.FormatoIncorreto, $"Formato de aula inválido");
			}

			return entity;
		}
	}
}
