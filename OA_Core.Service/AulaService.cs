﻿using AutoMapper;
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

			return _mapper.Map<IEnumerable<AulaResponse>>(listEntity);
		}

		public async Task<AulaResponse> ObterAulaPorIdAsync(Guid id)
		{
			var aula = await _aulaRepository.ObterPorIdAsync(id) ??
				throw new InformacaoException(StatusException.NaoEncontrado, $"Aula {id} não encontrado");

			return _mapper.Map<AulaResponse>(aula);
		}

		public async Task<Guid> CadastrarAulaAsync(AulaRequest request)
		{
			Aula entity;

			switch (request.Tipo)
			{
				case TipoAula.AulaOnline:
					entity = _mapper.Map<AulaOnline>(request);
					break;
				case TipoAula.AulaVideo:
					entity = _mapper.Map<AulaVideo>(request);
					break;
				case TipoAula.AulaTexto:
					entity = _mapper.Map<AulaTexto>(request);
					break;
				case TipoAula.AulaDownload:
					entity = _mapper.Map<AulaDownload>(request);
					break;
				default:
					throw new InformacaoException(StatusException.FormatoIncorreto, $"Formato de aula inválido");
			}

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

		public async Task EditarAulaAsync(Guid id, AulaRequestPut aulaRequest)
		{
			var entity = _mapper.Map<Aula>(aulaRequest);

			if (!entity.Valid)
			{
				_notificador.Handle(entity.ValidationResult);
				return;
			}

			var find = await _aulaRepository.ObterPorIdAsync(id) ??
				throw new InformacaoException(StatusException.NaoEncontrado, $"Aula {id} não encontrado");

			find.DataAlteracao = DateTime.Now;
			find.Titulo = entity.Titulo;
			find.Duracao = entity.Duracao;
			find.Tipo = entity.Tipo;
			find.Ordem = entity.Ordem;

			await _aulaRepository.EditarAsync(find);
		}
	}
}
