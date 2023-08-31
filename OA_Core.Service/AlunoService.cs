using AutoMapper;
using OA_Core.Domain.Contracts.Request;
using OA_Core.Domain.Contracts.Response;
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

        public async Task DeleteUsuarioAsync(Guid id)
        {
            var aluno = await _repository.FindAsync(id) ??
                throw new InformacaoException(StatusException.NaoEncontrado, $"Usuario {id} não encontrado");

            aluno.DataDelecao = DateTime.Now;
            await _repository.RemoveAsync(aluno);
        }

        public async Task<IEnumerable<AlunoResponse>> GetAllUsuariosAsync(int page, int rows)
        {
            var listEntity = await _repository.ListPaginationAsync(page, rows);

            return _mapper.Map<IEnumerable<AlunoResponse>>(listEntity);
        }

        public Task<AlunoResponse> GetUsuarioByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> PostUsuarioAsync(AlunoRequest alunoRequest)
        {
            throw new NotImplementedException();
        }

        public Task PutUsuarioAsync(Guid id, AlunoRequest alunoRequest)
        {
            throw new NotImplementedException();
        }
    }
}
