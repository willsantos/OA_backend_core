using AutoMapper;
using OA_Core.Domain.Contracts.Request;
using OA_Core.Domain.Contracts.Response;
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

        public ProfessorService(IMapper mapper, IProfessorRepository repository, INotificador notificador)
        {
            _mapper = mapper;
            _repository = repository;
            _notificador = notificador;
        }

        public Task DeleteProfessorAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UsuarioResponse>> GetAllProfessoresAsync(int page, int rows)
        {
            throw new NotImplementedException();
        }

        public Task<ProfessorResponse> GetProfessorByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> PostProfessorAsync(ProfessorRequest professorRequest)
        {
            throw new NotImplementedException();
        }

        public Task PutProfessorAsync(Guid id, ProfessorRequest professorRequest)
        {
            throw new NotImplementedException();
        }
    }
}
