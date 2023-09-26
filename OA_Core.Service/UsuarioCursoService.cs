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
        private readonly IUsuarioCursoRepository _usuarioCursoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
		private readonly ICursoRepository _cursoRepository;

		private readonly INotificador _notificador;

        public UsuarioCursoService(IMapper mapper, IUsuarioCursoRepository usuarioCursoRepository, IUsuarioRepository usuarioRepository, ICursoRepository cursoRepository, INotificador notificador)
        {
            _mapper = mapper;
            _usuarioCursoRepository = usuarioCursoRepository;
            _usuarioRepository = usuarioRepository;
			_cursoRepository = cursoRepository;
            _notificador = notificador;
        }


		//retorna uma lista dos cursos que ele está inscrito, com o status e o progresso de cada um, exibindo o progresso como porcentagem.
		public async Task<List<CursoParaUsuarioResponse>> GetCursoDeUsuarioByIdAsync(Guid usuarioId)
		{
			var curso = await _usuarioRepository.ObterPorIdAsync(usuarioId) ??
				throw new InformacaoException(StatusException.NaoEncontrado, $"Usuario {usuarioId} não encontrado");

			var usuarioCursos = await _usuarioCursoRepository.ObterTodosComIncludeAsync(x => x.UsuarioId == usuarioId);
			if (usuarioCursos.Count() <= 0)
				throw new InformacaoException(StatusException.NaoEncontrado, $"UsuarioCurso {usuarioId} não encontrado");

			var cursoList = new List<CursoParaUsuarioResponse>();

			foreach (var cursos in usuarioCursos)
			{
				var response = _mapper.Map<CursoParaUsuarioResponse>(cursos.Curso);
				response.Status = cursos.Status;
				response.Progresso = cursos.Progresso;
				cursoList.Add(response);
			}

			return cursoList;
		}

		public async Task<Guid> PostUsuarioCursoAsync(UsuarioCursoRequest usuarioCursoRequest)
        {
            var entity = _mapper.Map<UsuarioCurso>(usuarioCursoRequest);

			if (await _usuarioRepository.ObterPorIdAsync(entity.UsuarioId) is null)
                throw new InformacaoException(StatusException.NaoEncontrado, $"UsuarioId: {entity.UsuarioId} inválido ou não existente");
			if (await _cursoRepository.ObterPorIdAsync(entity.CursoId) is null)
				throw new InformacaoException(StatusException.NaoEncontrado, $"CursoId: {entity.CursoId} inválido ou não existente");

			if (await _usuarioCursoRepository.ObterAsync(x => x.UsuarioId == entity.UsuarioId && x.CursoId == entity.CursoId) != null)
				throw new InformacaoException(StatusException.Conflito, $"Esse cadastro já foi realizado no banco");

			if (!entity.Valid)
				throw new InformacaoException(StatusException.FormatoIncorreto, $"{entity.ValidationResult}");
         
            await _usuarioCursoRepository.AdicionarAsync(entity);
            return entity.Id;
        }

    }
}
