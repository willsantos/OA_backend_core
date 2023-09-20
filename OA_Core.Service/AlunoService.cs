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
        private readonly IAlunoRepository _alunoRepository;
		private readonly IUsuarioRepository _usuarioRepository;
		private readonly IMapper _mapper;
        private readonly INotificador _notificador;

		public AlunoService(IAlunoRepository repository, IUsuarioRepository usuarioRepository, IMapper mapper, INotificador notificador)
        {
            _alunoRepository = repository;
			_usuarioRepository = usuarioRepository;
			_mapper = mapper;
            _notificador = notificador;
        }

        public async Task DeleteAlunoAsync(Guid id)
        {
            var aluno = await _alunoRepository.ObterPorIdAsync(id) ??
                throw new InformacaoException(StatusException.NaoEncontrado, $"Usuario {id} não encontrado");

            aluno.DataDelecao = DateTime.Now;
            await _alunoRepository.RemoverAsync(aluno);
        }

        public async Task<IEnumerable<AlunoResponse>> GetAllAlunosAsync(int page, int rows)
        {
            var listEntity = await _alunoRepository.ObterTodosAsync(page, rows);

            return _mapper.Map<IEnumerable<AlunoResponse>>(listEntity);
        }

        public async Task<AlunoResponse> GetAlunoByIdAsync(Guid id)
        {
            var usuario = await _alunoRepository.ObterPorIdAsync(id) ??
                throw new InformacaoException(StatusException.NaoEncontrado, $"Aluno {id} não encontrado");

            return _mapper.Map<AlunoResponse>(usuario);
        }

        public async Task<Guid> PostAlunoAsync(AlunoRequest alunoRequest)
        {
            var entity = _mapper.Map<Aluno>(alunoRequest);
			alunoRequest.Cpf.Verificar();
			entity.Cpf = alunoRequest.Cpf.Exibir();

			if (await _usuarioRepository.FindAsync(alunoRequest.UsuarioId)is null)
				throw new InformacaoException(StatusException.NaoEncontrado, $"UsuarioId {alunoRequest.UsuarioId} inválido ou não existente");

			var existingAlunoWithCpf = await _alunoRepository.FindByCpfAsync(entity.Cpf);
			if (existingAlunoWithCpf != null)
				throw new InformacaoException(StatusException.Conflito, $"CPF {entity.Cpf} incorreto.");

			if (!entity.Valid)
            {
                _notificador.Handle(entity.ValidationResult);
                return Guid.Empty;
            }

            await _alunoRepository.AdicionarAsync(entity);
            return entity.Id;
        }

        public async Task PutAlunoAsync(Guid id, AlunoRequestPut alunoRequest)
        {

			var entity = _mapper.Map<Aluno>(alunoRequest);

			alunoRequest.Cpf.Verificar();

			var existingAlunoWithCpf = await _alunoRepository.FindByCpfAsync(entity.Cpf);
			if (existingAlunoWithCpf != null)
				throw new InformacaoException(StatusException.Conflito, $"CPF {entity.Cpf} incorreto.");

			if (!entity.Valid)
			{
				_notificador.Handle(entity.ValidationResult);
				return;
			}

			var find = await _alunoRepository.ObterPorIdAsync(id) ??
				throw new InformacaoException(StatusException.NaoEncontrado, $"Aluno {id} não encontrado");

			find.Cpf = alunoRequest.Cpf.Exibir();
			find.Foto = alunoRequest.Foto;
			find.DataAlteracao = DateTime.Now;
			await _alunoRepository.EditarAsync(find);
		}
    }
}
