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

        public async Task DeleteAulaAsync(Guid id)
        {
            var aula = await _aulaRepository.ObterPorIdAsync(id) ??
                throw new InformacaoException(StatusException.NaoEncontrado, $"Aula {id} não encontrado");

            aula.DataDelecao = DateTime.Now;
            await _aulaRepository.RemoverAsync(aula);
        }

        public async Task<IEnumerable<AulaResponse>> GetAllAulasAsync(int page, int rows)
        {
            var listEntity = await _aulaRepository.ObterTodosAsync(page, rows);

            return _mapper.Map<IEnumerable<AulaResponse>>(listEntity);
        }

        public async Task<AulaResponse> GetAulaByIdAsync(Guid id)
        {
            var aula = await _aulaRepository.ObterPorIdAsync(id) ??
                throw new InformacaoException(StatusException.NaoEncontrado, $"Aula {id} não encontrado");

            return _mapper.Map<AulaResponse>(aula);
        }

        public async Task<Guid> PostAulaAsync(AulaRequest aulaRequest)
        {
            var entity = _mapper.Map<Aula>(aulaRequest);
       
            if (await _cursoRepository.ObterPorIdAsync(aulaRequest.CursoId) is null)
                throw new InformacaoException(StatusException.NaoEncontrado, $"CursoId: {aulaRequest.CursoId} inválido ou não existente");
            
            if (!entity.Valid)
            {
                _notificador.Handle(entity.ValidationResult);
                return Guid.Empty;

            }          

            await _aulaRepository.AdicionarAsync(entity);
            return entity.Id;
        }

        public async Task PutAulaAsync(Guid id, AulaRequestPut aulaRequest)
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
			find.Nome = entity.Nome;
			find.Duracao = entity.Duracao;
			find.Descricao = entity.Descricao;
			find.Caminho = entity.Caminho;
			find.Tipo = entity.Tipo;
			find.Ordem = entity.Ordem;

            await _aulaRepository.EditarAsync(find);
        }
    }
}
