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
    public class ProfessorService : IProfessorService
    {
        private readonly IMapper _mapper;
        private readonly IProfessorRepository _repository;
        private readonly INotificador _notificador;
        private readonly IUsuarioRepository _usuarioRepository;

        public ProfessorService(IMapper mapper, IProfessorRepository repository,IUsuarioRepository usuarioRepository, INotificador notificador)
        {
            _mapper = mapper;
            _repository = repository;
            _notificador = notificador;
            _usuarioRepository = usuarioRepository;            
        }

        public async Task DeletarProfessorAsync(Guid id)
        {
            var professor = await _repository.ObterPorIdAsync(id) ??
                throw new InformacaoException(StatusException.NaoEncontrado, $"Usuario {id} não encontrado");

            professor.DataDelecao = DateTime.Now;
            await _repository.EditarAsync(professor);
        }

        public async Task<IEnumerable<ProfessorResponse>> ObterTodosProfessoresAsync(int page, int rows)
        {
            var listEntity = await _repository.ObterTodosAsync(page, rows);

            return _mapper.Map<IEnumerable<ProfessorResponse>>(listEntity);
        }

        public async Task<ProfessorResponse> ObterProfessorPorIdAsync(Guid id)
        {
            var professor = await _repository.ObterPorIdAsync(id) ??
                throw new InformacaoException(StatusException.NaoEncontrado, $"Professor {id} não encontrado");

            return _mapper.Map<ProfessorResponse>(professor);
        }

        public async Task<Guid> CadastrarProfessorAsync(ProfessorRequest professorRequest)
        {
            var entity = _mapper.Map<Professor>(professorRequest);
       
            if (await _usuarioRepository.ObterPorIdAsync(professorRequest.UsuarioId) is null)
                throw new InformacaoException(StatusException.NaoEncontrado, $"UsuarioId {professorRequest.UsuarioId} inválido ou não existente");
            
            if (!entity.Valid)
            {
                _notificador.Handle(entity.ValidationResult);
                return Guid.Empty;

            }          

            await _repository.AdicionarAsync(entity);
            return entity.Id;
        }

        public async Task EditarProfessorAsync(Guid id, ProfessorRequestPut professorRequest)
        {
            var entity = _mapper.Map<Professor>(professorRequest);   

            if (!entity.Valid)
            {
                _notificador.Handle(entity.ValidationResult);
                return;
            }

            var find = await _repository.ObterPorIdAsync(id) ??
                throw new InformacaoException(StatusException.NaoEncontrado, $"Professor {id} não encontrado");

			find.Formacao = entity.Formacao;
			find.Experiencia = entity.Experiencia;
			find.Foto = entity.Foto;
			find.Biografia = entity.Biografia;

            await _repository.EditarAsync(find);
        }
    }
}
