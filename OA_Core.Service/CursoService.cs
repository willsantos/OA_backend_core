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
    public class CursoService : ICursoService
    {
        private readonly IMapper _mapper;
        private readonly ICursoRepository _cursoRepository;
        private readonly IProfessorRepository _professorRepository;
        private readonly INotificador _notificador;

        public CursoService(IMapper mapper, ICursoRepository cursoRepository, IProfessorRepository professorRepository, INotificador notificador)
        {
            _mapper = mapper;
            _cursoRepository = cursoRepository;
            _professorRepository = professorRepository;
            _notificador = notificador;
        }

        public async Task DeleteCursoAsync(Guid id)
        {
            var curso = await _cursoRepository.FindAsync(id) ??
                throw new InformacaoException(StatusException.NaoEncontrado, $"Curso {id} não encontrado");

            curso.DataDelecao = DateTime.Now;
            await _cursoRepository.RemoveAsync(curso);
        }

        public async Task<IEnumerable<CursoResponse>> GetAllCursosAsync(int page, int rows)
        {
            var listEntity = await _cursoRepository.ListPaginationAsync(page, rows);

            return _mapper.Map<IEnumerable<CursoResponse>>(listEntity);
        }

        public async Task<CursoResponse> GetCursoByIdAsync(Guid id)
        {
            var curso = await _cursoRepository.FindAsync(id) ??
                throw new InformacaoException(StatusException.NaoEncontrado, $"Curso {id} não encontrado");

            return _mapper.Map<CursoResponse>(curso);
        }

        public async Task<Guid> PostCursoAsync(CursoRequest cursoRequest)
        {
            var entity = _mapper.Map<Curso>(cursoRequest);
       
            if (await _professorRepository.FindAsync(cursoRequest.ProfessorId) is null)
                throw new InformacaoException(StatusException.NaoEncontrado, $"ProfessorId: {cursoRequest.ProfessorId} inválido ou não existente");
            
            if (!entity.Valid)
            {
                _notificador.Handle(entity.ValidationResult);
                return Guid.Empty;

            }          

            await _cursoRepository.AddAsync(entity);
            return entity.Id;
        }

        public async Task PutCursoAsync(Guid id, CursoRequestPut cursoRequest)
        {
            var entity = _mapper.Map<Curso>(cursoRequest);   

            if (!entity.Valid)
            {
                _notificador.Handle(entity.ValidationResult);
                return;
            }

            var find = await _cursoRepository.FindAsync(id) ??
                throw new InformacaoException(StatusException.NaoEncontrado, $"Curso {id} não encontrado");
            entity.Id = find.Id;
            entity.ProfessorId = find.ProfessorId;
            entity.DataCriacao = find.DataCriacao;
            entity.DataAlteracao = DateTime.Now;

            await _cursoRepository.EditAsync(entity);
        }
    }
}
