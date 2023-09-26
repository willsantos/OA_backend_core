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
    public class UsuarioCursoService : IUsuarioCursoService
    {
        private readonly IMapper _mapper;
        private readonly IUsuarioCursoRepository _cursoUsuarioRepository;
        private readonly IUsuarioRepository _usuarioRepository;
		private readonly ICursoRepository _cursoRepository;

		private readonly INotificador _notificador;

        public UsuarioCursoService(IMapper mapper, IUsuarioCursoRepository cursoUsuarioRepository, IUsuarioRepository usuarioRepository, ICursoRepository cursoRepository, INotificador notificador)
        {
            _mapper = mapper;
            _cursoUsuarioRepository = cursoUsuarioRepository;
            _usuarioRepository = usuarioRepository;
			_cursoRepository = cursoRepository;
            _notificador = notificador;
        }

        public async Task DeleteUsuarioCursoAsync(Guid cursoId, Guid usuarioId)
        {
            var cursoUsuario = await _cursoUsuarioRepository.ObterAsync(x => x.CursoId == cursoId && x.UsuarioId == usuarioId) ??
                throw new InformacaoException(StatusException.NaoEncontrado, $"UsuarioCurso não encontrado");			

            cursoUsuario.DataDelecao = DateTime.Now;
            await _cursoUsuarioRepository.EditarAsync(cursoUsuario);
        }

        public async Task<IEnumerable<UsuarioCurso>> GetAllUsuarioCursosAsync(int page, int rows)
        {
            var listEntity = await _cursoUsuarioRepository.ObterTodosAsync(page, rows);

            return _mapper.Map<IEnumerable<UsuarioCurso>>(listEntity);
        }

        public async Task<UsuarioCurso> GetUsuarioCursoByIdAsync(Guid id)
        {
            var cursoUsuario = await _cursoUsuarioRepository.ObterPorIdAsync(id) ??
                throw new InformacaoException(StatusException.NaoEncontrado, $"UsuarioCurso {id} não encontrado");

            return _mapper.Map<UsuarioCurso>(cursoUsuario);
        }

		//retorna uma lista dos cursos que ele está inscrito, com o status e o progresso de cada um, exibindo o progresso como porcentagem.
		//public async Task<List<UsuarioResponseComResponsavel>> GetUsuarioDeCursoByIdAsync(Guid cursoId)
		//{
		//	var curso = await _cursoRepository.ObterPorIdAsync(cursoId) ??
		//		throw new InformacaoException(StatusException.NaoEncontrado, $"Curso {cursoId} não encontrado");

		//	var cursoUsuarioes = await _cursoUsuarioRepository.ObterTodosComIncludeAsync(x => x.CursoId == cursoId);
		//	if (cursoUsuarioes.Count() <= 0)
		//		throw new InformacaoException(StatusException.NaoEncontrado, $"UsuarioCurso {cursoId} não encontrado");

		//	var usuarioes = new List<UsuarioResponseComResponsavel>();

		//	foreach( var usuario in cursoUsuarioes )
		//	{
		//		var response = _mapper.Map<UsuarioResponseComResponsavel>(usuario.Usuario);
		//		response.Responsavel = usuario.Responsavel;
		//		usuarioes.Add(response);
		//	}

		//	return usuarioes;
		//}

		public async Task<Guid> PostUsuarioCursoAsync(UsuarioCursoRequest cursoUsuarioRequest, Guid cursoId)
        {
            var entity = _mapper.Map<UsuarioCurso>(cursoUsuarioRequest);
			entity.CursoId = cursoId;


			if (await _usuarioRepository.ObterPorIdAsync(cursoUsuarioRequest.UsuarioId) is null)
                throw new InformacaoException(StatusException.NaoEncontrado, $"UsuarioId: {cursoUsuarioRequest.UsuarioId} inválido ou não existente");
			if (await _cursoRepository.ObterPorIdAsync(cursoId) is null)
				throw new InformacaoException(StatusException.NaoEncontrado, $"CursoId: {cursoId} inválido ou não existente");

			if (!entity.Valid)
				throw new InformacaoException(StatusException.FormatoIncorreto, $"{entity.ValidationResult}");
         
            await _cursoUsuarioRepository.AdicionarAsync(entity);
            return entity.Id;
        }

        public async Task PutUsuarioCursoAsync(Guid cursoId, UsuarioCursoRequest cursoUsuarioRequest)
        {
            var entity = _mapper.Map<UsuarioCurso>(cursoUsuarioRequest);   
			entity.CursoId = cursoId;

            if (!entity.Valid)
            {
                _notificador.Handle(entity.ValidationResult);
                return;
            }

			var find = await _cursoUsuarioRepository.ObterAsync(x => x.CursoId == cursoId && x.UsuarioId == entity.UsuarioId) ??
				throw new InformacaoException(StatusException.NaoEncontrado, $"UsuarioCurso não encontrado");

			find.CursoId = entity.CursoId;
			find.UsuarioId = entity.UsuarioId;
			find.Responsavel = entity.Responsavel;
            find.DataAlteracao = DateTime.Now;

            await _cursoUsuarioRepository.EditarAsync(find);
        }
    }
}
