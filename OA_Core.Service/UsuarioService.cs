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
    public class UsuarioService : IUsuarioService
    {
        private readonly IMapper _mapper;
        private readonly IUsuarioRepository _repository;
        private readonly INotificador _notificador;

        public UsuarioService(IUsuarioRepository repository, INotificador notificador, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
            _notificador = notificador;
        }
        public async Task DeleteUsuarioAsync(Guid id)
        {
            var usuario = await _repository.FindAsync(id) ??
                throw new InformacaoException(StatusException.NaoEncontrado, $"Usuario {id} não encontrado");

            usuario.DataDelecao = DateTime.Now;
            await _repository.RemoveAsync(usuario);
        }

        public async Task<IEnumerable<UsuarioResponse>> GetAllUsuariosAsync(int page, int rows)
        {
            var listEntity = await _repository.ListPaginationAsync(page, rows);

            return _mapper.Map<IEnumerable<UsuarioResponse>>(listEntity);
        }

        public async Task<UsuarioResponse> GetUsuarioByIdAsync(Guid id)
        {
            var usuario = await _repository.FindAsync(id) ?? 
                throw new InformacaoException(StatusException.NaoEncontrado, $"Usuario {id} não encontrado");

            return _mapper.Map<UsuarioResponse>(usuario);
        }

        public async Task<Guid> PostUsuarioAsync(UsuarioRequest usuarioRequest)
        {
			//TODO: Encryptar senha
            //TODO: Mandar email confirmacao usuario
            var entity = _mapper.Map<Usuario>(usuarioRequest);

            if (!entity.Valid)
            {
                _notificador.Handle(entity.ValidationResult);
                return Guid.Empty;

            }

            await _repository.AddAsync(entity);
            return entity.Id;
        }

        public async Task PutUsuarioAsync(Guid id, UsuarioRequest usuarioRequest)
        {
			//TODO: Fazer verificacoes de seguranca para permitir a edicao a partir de quem estiver logado no sistema
            var entity = _mapper.Map<Usuario>(usuarioRequest);

            if (!entity.Valid)
            {
                _notificador.Handle(entity.ValidationResult);
                return;
            }

            var find = await _repository.FindAsync(id) ??
                throw new InformacaoException(StatusException.NaoEncontrado, $"Usuario {id} não encontrado");

            entity.Id = find.Id;
            entity.DataCriacao = find.DataCriacao;
            entity.DataAlteracao = DateTime.Now;

            await _repository.EditAsync(entity);
        }
    }
}
