using AutoFixture;
using AutoMapper;
using NSubstitute;
using OA_Core.Domain.Contracts.Request;
using OA_Core.Domain.Entities;
using OA_Core.Domain.Exceptions;
using OA_Core.Domain.Interfaces.Notifications;
using OA_Core.Domain.Interfaces.Repository;
using OA_Core.Service;
using OA_Core.Tests.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OA_Core.Tests.Service
{
    [Trait("Service", "UsuarioCurso Service")]
    public class UsuarioCursoServiceTest
    {
        private readonly IMapper _mapper;
        private readonly Fixture _fixture;
        private readonly INotificador _notifier;
		private readonly IUsuarioRepository _usuarioRepository;
		private readonly ICursoRepository _cursoRepository;
		private readonly IUsuarioCursoRepository _cursoUsuarioRepository;

        public UsuarioCursoServiceTest()
        {
            _fixture = FixtureConfig.GetFixture();
            _mapper = MapperConfig.Get();
            _notifier = Substitute.For<INotificador>();
			_usuarioRepository = Substitute.For<IUsuarioRepository>();
			_cursoRepository = Substitute.For<ICursoRepository>();
			_cursoUsuarioRepository = Substitute.For<IUsuarioCursoRepository>();
        }

        [Fact(DisplayName = "Cria um UsuarioCurso Válido")]
        public async Task CriarUsuarioCurso()
        {                       
            var cursoUsuarioService = new UsuarioCursoService(_mapper, _cursoUsuarioRepository, _usuarioRepository, _cursoRepository, _notifier);

            var usuario = _fixture.Create<Usuario>();
			var curso = _fixture.Create<Curso>();

			var cursoUsuarioRequest = new UsuarioCursoRequest
			{
				UsuarioId = Guid.NewGuid(),
				CursoId = Guid.NewGuid(),
				Progresso = 0,
				Status = 0,				
			};

			_usuarioRepository.ObterPorIdAsync(Arg.Any<Guid>()).Returns(usuario);
            _cursoRepository.ObterPorIdAsync(Arg.Any<Guid>()).Returns(curso);
			_cursoUsuarioRepository.AdicionarAsync(Arg.Any<UsuarioCurso>()).Returns(Task.CompletedTask);

			var result = await cursoUsuarioService.PostUsuarioCursoAsync(cursoUsuarioRequest);
			Assert.NotNull(result);
			Assert.IsType<Guid>(result);
		}

		[Fact(DisplayName = "Cria um UsuarioCurso Válido")]
		public async Task CriarUsuarioCursoInvalido()
		{
			var cursoUsuarioService = new UsuarioCursoService(_mapper, _cursoUsuarioRepository, _usuarioRepository, _cursoRepository, _notifier);

			var usuario = _fixture.Create<Usuario>();
			var curso = _fixture.Create<Curso>();
			var usuarioCurso = _fixture.Create<UsuarioCurso>();

			var cursoUsuarioRequest = new UsuarioCursoRequest
			{
				UsuarioId = Guid.NewGuid(),
				CursoId = Guid.NewGuid(),
				Progresso = 0,
				Status = 0,
			};

			_usuarioRepository.ObterPorIdAsync(Arg.Any<Guid>()).Returns(usuario);
			_cursoRepository.ObterPorIdAsync(Arg.Any<Guid>()).Returns(curso);
			_cursoUsuarioRepository.ObterAsync(Arg.Any<Expression<Func<UsuarioCurso, bool>>>()).Returns(usuarioCurso);
			_cursoUsuarioRepository.AdicionarAsync(Arg.Any<UsuarioCurso>()).Returns(Task.CompletedTask);

			await Assert.ThrowsAsync<InformacaoException>(() => cursoUsuarioService.PostUsuarioCursoAsync(cursoUsuarioRequest));;
		}

		[Fact(DisplayName = "Cria um UsuarioCurso com UsuarioId inválido")]
		public async Task CriarUsuarioCursoComUsuarioIdInvalido()
		{

			var cursoUsuarioService = new UsuarioCursoService(_mapper, _cursoUsuarioRepository, _usuarioRepository, _cursoRepository, _notifier);

			var cursoUsuarioRequest = _fixture.Create<UsuarioCursoRequest>();

			await Assert.ThrowsAsync<InformacaoException>(() => cursoUsuarioService.PostUsuarioCursoAsync(cursoUsuarioRequest));
		}

		[Fact(DisplayName = "Cria um UsuarioCurso com UsuarioId inválido")]
		public async Task CriarUsuarioCursoComCursoIdInvalido()
		{

			var cursoUsuarioService = new UsuarioCursoService(_mapper, _cursoUsuarioRepository, _usuarioRepository, _cursoRepository, _notifier);

			var usuario = _fixture.Create<Usuario>();
			var cursoUsuarioRequest = _fixture.Create<UsuarioCursoRequest>();

			_usuarioRepository.ObterPorIdAsync(Arg.Any<Guid>()).Returns(usuario);


			await Assert.ThrowsAsync<InformacaoException>(() => cursoUsuarioService.PostUsuarioCursoAsync(cursoUsuarioRequest));
		}		

		[Fact(DisplayName = "Obtém todos os UsuarioCurso por expressão")]
		public async Task ObterTodosUsuarioCursoPorExpressão()
		{
			var cursoUsuarioService = new UsuarioCursoService(_mapper, _cursoUsuarioRepository, _usuarioRepository, _cursoRepository, _notifier);

			var curso = _fixture.Create<Curso>();
			var usuario = _fixture.Create<Usuario>();

			var cursoUsuarios = _fixture.CreateMany<UsuarioCurso>(5);

			_cursoRepository.ObterPorIdAsync(Arg.Any<Guid>()).Returns(curso);
			_usuarioRepository.ObterPorIdAsync(Arg.Any<Guid>()).Returns(usuario);

			_cursoUsuarioRepository.ObterTodosComIncludeAsync(Arg.Any<Expression<Func<UsuarioCurso, bool>>>()).Returns(cursoUsuarios);

			var result = await cursoUsuarioService.GetCursoDeUsuarioByIdAsync(Guid.NewGuid());

			Assert.Equal(5, result.Count());
		}

		[Fact(DisplayName = "Obtém todos os UsuarioCurso por expressão com curso invalido")]
		public async Task ObterTodosUsuarioCursoPorExpressãoCursoInvalido()
		{
			var cursoUsuarioService = new UsuarioCursoService(_mapper, _cursoUsuarioRepository, _usuarioRepository, _cursoRepository, _notifier);

			var cursoUsuarios = _fixture.CreateMany<UsuarioCurso>(5);

			_cursoUsuarioRepository.ObterTodosComIncludeAsync(Arg.Any<Expression<Func<UsuarioCurso, bool>>>()).Returns(cursoUsuarios);

			await Assert.ThrowsAsync<InformacaoException>(() => cursoUsuarioService.GetCursoDeUsuarioByIdAsync(Guid.NewGuid()));
		}

		[Fact(DisplayName = "Obtém todos os UsuarioCurso por expressão com cursousuario invalido")]
		public async Task ObterTodosUsuarioCursoPorExpressãoUsuarioCursoInvalido()
		{
			var cursoUsuarioService = new UsuarioCursoService(_mapper, _cursoUsuarioRepository, _usuarioRepository, _cursoRepository, _notifier);

			var curso = _fixture.Create<Curso>();
			var usuario = _fixture.Create<Usuario>();

			var cursoUsuarios = _fixture.CreateMany<UsuarioCurso>(0);

			_usuarioRepository.ObterPorIdAsync(Arg.Any<Guid>()).Returns(usuario);
			_cursoRepository.ObterPorIdAsync(Arg.Any<Guid>()).Returns(curso);
			_cursoUsuarioRepository.ObterTodosComIncludeAsync(Arg.Any<Expression<Func<UsuarioCurso, bool>>>()).Returns(cursoUsuarios);

			await Assert.ThrowsAsync<InformacaoException>(() => cursoUsuarioService.GetCursoDeUsuarioByIdAsync(Guid.NewGuid()));
		}	 
    }
}
