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
    public class AlunoService : IAlunoService
    {
        private readonly IAlunoRepository _repository;
        private readonly IMapper _mapper;
        private readonly INotificador _notificador;

        public AlunoService(IAlunoRepository repository, IMapper mapper, INotificador notificador)
        {
            _repository = repository;
            _mapper = mapper;
            _notificador = notificador;
        }

        public async Task DeleteAlunoAsync(Guid id)
        {
            var aluno = await _repository.FindAsync(id) ??
                throw new InformacaoException(StatusException.NaoEncontrado, $"Usuario {id} não encontrado");

            aluno.DataDelecao = DateTime.Now;
            await _repository.RemoveAsync(aluno);
        }

        public async Task<IEnumerable<AlunoResponse>> GetAllAlunosAsync(int page, int rows)
        {
            var listEntity = await _repository.ListPaginationAsync(page, rows);

            return _mapper.Map<IEnumerable<AlunoResponse>>(listEntity);
        }

        public async Task<AlunoResponse> GetAlunoByIdAsync(Guid id)
        {
            var usuario = await _repository.FindAsync(id) ??
                throw new InformacaoException(StatusException.NaoEncontrado, $"Aluno {id} não encontrado");

            return _mapper.Map<AlunoResponse>(usuario);
        }

        public async Task<Guid> PostAlunoAsync(AlunoRequest alunoRequest)
        {
            var entity = _mapper.Map<Aluno>(alunoRequest);

            if (!entity.Valid)
            {
                _notificador.Handle(entity.ValidationResult);
                return Guid.Empty;
            }

            await _repository.AddAsync(entity);
            return entity.Id;
        }

        public async Task PutAlunoAsync(Guid id, AlunoRequest alunoRequest)
        {
            var entity = _mapper.Map<Aluno>(alunoRequest);

            if (!entity.Valid)
            {
                _notificador.Handle(entity.ValidationResult);
                return;
            }

            var find = await _repository.FindAsync(id) ??
                throw new InformacaoException(StatusException.NaoEncontrado, $"Aluno {id} não encontrado");

            entity.Id = find.Id;
            entity.DataCriacao = find.DataCriacao;
            entity.DataAlteracao = DateTime.Now;

            await _repository.EditAsync(entity);
        }
    }
}
