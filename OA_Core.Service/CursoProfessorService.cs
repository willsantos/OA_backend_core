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
    public class CursoProfessorService : ICursoProfessorService
    {
        private readonly IMapper _mapper;
        private readonly ICursoProfessorRepository _cursoProfessorRepository;
        private readonly IProfessorRepository _professorRepository;
		private readonly ICursoRepository _cursoRepository;

		private readonly INotificador _notificador;

        public CursoProfessorService(IMapper mapper, ICursoProfessorRepository cursoProfessorRepository, IProfessorRepository professorRepository, ICursoRepository cursoRepository, INotificador notificador)
        {
            _mapper = mapper;
            _cursoProfessorRepository = cursoProfessorRepository;
            _professorRepository = professorRepository;
			_cursoRepository = cursoRepository;
            _notificador = notificador;
        }

        public async Task DeletarCursoProfessorAsync(Guid cursoId, Guid professorId)
        {
            var cursoProfessor = await _cursoProfessorRepository.ObterAsync(x => x.CursoId == cursoId && x.ProfessorId == professorId) ??
                throw new InformacaoException(StatusException.NaoEncontrado, $"CursoProfessor não encontrado");

			if (cursoProfessor.Responsavel)
				throw new InformacaoException(StatusException.Erro, $"Professor Responsável não pode ser removido");

            cursoProfessor.DataDelecao = DateTime.Now;
            await _cursoProfessorRepository.EditarAsync(cursoProfessor);
        }

        public async Task<IEnumerable<CursoProfessor>> ObterTodosCursoProfessoresAsync(int page, int rows)
        {
            var listEntity = await _cursoProfessorRepository.ObterTodosAsync(page, rows);

            return _mapper.Map<IEnumerable<CursoProfessor>>(listEntity);
        }

        public async Task<CursoProfessor> ObterCursoProfessorPorIdAsync(Guid id)
        {
            var cursoProfessor = await _cursoProfessorRepository.ObterPorIdAsync(id) ??
                throw new InformacaoException(StatusException.NaoEncontrado, $"CursoProfessor {id} não encontrado");

            return _mapper.Map<CursoProfessor>(cursoProfessor);
        }

		public async Task<List<ProfessorResponseComResponsavel>> ObterProfessoresDeCursoPorIdAsync(Guid cursoId)
		{
			var curso = await _cursoRepository.ObterPorIdAsync(cursoId) ??
				throw new InformacaoException(StatusException.NaoEncontrado, $"Curso {cursoId} não encontrado");

			var cursoProfessores = await _cursoProfessorRepository.ObterTodosComIncludeAsync(x => x.CursoId == cursoId);
			if (cursoProfessores.Count() <= 0)
				throw new InformacaoException(StatusException.NaoEncontrado, $"CursoProfessor {cursoId} não encontrado");

			var professores = new List<ProfessorResponseComResponsavel>();

			foreach( var professor in cursoProfessores )
			{
				var response = _mapper.Map<ProfessorResponseComResponsavel>(professor.Professor);
				response.Responsavel = professor.Responsavel;
				professores.Add(response);
			}

			return professores;
		}

		public async Task<Guid> CadastrarCursoProfessorAsync(CursoProfessorRequest cursoProfessorRequest, Guid cursoId)
        {
            var entity = _mapper.Map<CursoProfessor>(cursoProfessorRequest);
			entity.CursoId = cursoId;


			if (await _professorRepository.ObterPorIdAsync(cursoProfessorRequest.ProfessorId) is null)
                throw new InformacaoException(StatusException.NaoEncontrado, $"ProfessorId: {cursoProfessorRequest.ProfessorId} inválido ou não existente");
			if (await _cursoRepository.ObterPorIdAsync(cursoId) is null)
				throw new InformacaoException(StatusException.NaoEncontrado, $"CursoId: {cursoId} inválido ou não existente");

			if (!entity.Valid)
				throw new InformacaoException(StatusException.FormatoIncorreto, $"{entity.ValidationResult}");
         
            await _cursoProfessorRepository.AdicionarAsync(entity);
            return entity.Id;
        }

        public async Task EditarCursoProfessorAsync(Guid cursoId, CursoProfessorRequest cursoProfessorRequest)
        {
            var entity = _mapper.Map<CursoProfessor>(cursoProfessorRequest);   
			entity.CursoId = cursoId;

            if (!entity.Valid)
            {
                _notificador.Handle(entity.ValidationResult);
                return;
            }

			var find = await _cursoProfessorRepository.ObterAsync(x => x.CursoId == cursoId && x.ProfessorId == entity.ProfessorId) ??
				throw new InformacaoException(StatusException.NaoEncontrado, $"CursoProfessor não encontrado");

			find.CursoId = entity.CursoId;
			find.ProfessorId = entity.ProfessorId;
			find.Responsavel = entity.Responsavel;

            await _cursoProfessorRepository.EditarAsync(find);
        }
    }
}
