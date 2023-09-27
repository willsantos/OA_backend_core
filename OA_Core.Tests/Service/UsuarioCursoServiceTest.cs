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

		[Fact(DisplayName = "CriarUsuarioCurso_Valido_DeveRetornarId")]
		public async Task CriarUsuarioCurso_Valido_DeveRetornarId()
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

			var result = await cursoUsuarioService.CadastrarUsuarioACursoAsync(cursoUsuarioRequest);
			Assert.NotNull(result);
			Assert.IsType<Guid>(result);
		}

		[Fact(DisplayName = "CriarUsuarioCurso_ComUsuarioIdNulo_DeveLancarExcecao")]
		public async Task CriarUsuarioCurso_ComUsuarioIdNulo_DeveLancarExcecao()
		{
			var cursoUsuarioService = new UsuarioCursoService(_mapper, _cursoUsuarioRepository, _usuarioRepository, _cursoRepository, _notifier);

			var usuario = _fixture.Create<Usuario>();
			var curso = _fixture.Create<Curso>();

			var cursoUsuarioRequest = new UsuarioCursoRequest
			{
				CursoId = Guid.NewGuid(),
				Progresso = 0,
				Status = 0,
			};

			_usuarioRepository.ObterPorIdAsync(Arg.Any<Guid>()).Returns(usuario);
			_cursoRepository.ObterPorIdAsync(Arg.Any<Guid>()).Returns(curso);
			_cursoUsuarioRepository.AdicionarAsync(Arg.Any<UsuarioCurso>()).Returns(Task.CompletedTask);

			await Assert.ThrowsAsync<InformacaoException>(() => cursoUsuarioService.CadastrarUsuarioACursoAsync(cursoUsuarioRequest)); ;
		}

		[Fact(DisplayName = "CriarUsuarioCurso_Invalido_DeveLancarExcecao")]
		public async Task CriarUsuarioCurso_Invalido_DeveLancarExcecao()
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

			await Assert.ThrowsAsync<InformacaoException>(() => cursoUsuarioService.CadastrarUsuarioACursoAsync(cursoUsuarioRequest)); ;
		}

		[Fact(DisplayName = "CriarUsuarioCurso_ComUsuarioIdInvalido_DeveLancarExcecao")]
		public async Task CriarUsuarioCurso_ComUsuarioIdInvalido_DeveLancarExcecao()
		{

			var cursoUsuarioService = new UsuarioCursoService(_mapper, _cursoUsuarioRepository, _usuarioRepository, _cursoRepository, _notifier);

			var cursoUsuarioRequest = _fixture.Create<UsuarioCursoRequest>();

			await Assert.ThrowsAsync<InformacaoException>(() => cursoUsuarioService.CadastrarUsuarioACursoAsync(cursoUsuarioRequest));
		}

		[Fact(DisplayName = "CriarUsuarioCurso_ComCursoIdInvalido_DeveLancarExcecao")]
		public async Task CriarUsuarioCurso_ComCursoIdInvalido_DeveLancarExcecao()
		{

			var cursoUsuarioService = new UsuarioCursoService(_mapper, _cursoUsuarioRepository, _usuarioRepository, _cursoRepository, _notifier);

			var usuario = _fixture.Create<Usuario>();
			var cursoUsuarioRequest = _fixture.Create<UsuarioCursoRequest>();

			_usuarioRepository.ObterPorIdAsync(Arg.Any<Guid>()).Returns(usuario);


			await Assert.ThrowsAsync<InformacaoException>(() => cursoUsuarioService.CadastrarUsuarioACursoAsync(cursoUsuarioRequest));
		}

		[Fact(DisplayName = "ObterTodosUsuarioCursoPorExpressao_DeveRetornarLista")]
		public async Task ObterTodosUsuarioCursoPorExpressao_DeveRetornarLista()
		{
			var cursoUsuarioService = new UsuarioCursoService(_mapper, _cursoUsuarioRepository, _usuarioRepository, _cursoRepository, _notifier);

			var curso = _fixture.Create<Curso>();
			var usuario = _fixture.Create<Usuario>();

			var cursoUsuarios = _fixture.CreateMany<UsuarioCurso>(5);

			_cursoRepository.ObterPorIdAsync(Arg.Any<Guid>()).Returns(curso);
			_usuarioRepository.ObterPorIdAsync(Arg.Any<Guid>()).Returns(usuario);

			_cursoUsuarioRepository.ObterTodosComIncludeAsync(Arg.Any<Expression<Func<UsuarioCurso, bool>>>()).Returns(cursoUsuarios);

			var result = await cursoUsuarioService.ObterCursosDeUsuarioPorIdAsync(Guid.NewGuid());

			Assert.Equal(5, result.Count());
		}

		[Fact(DisplayName = "ObterTodosUsuarioCursoPorExpressao_ComCursoInvalido_DeveLancarExcecao")]
		public async Task ObterTodosUsuarioCursoPorExpressao_ComCursoInvalido_DeveLancarExcecao()
		{
			var cursoUsuarioService = new UsuarioCursoService(_mapper, _cursoUsuarioRepository, _usuarioRepository, _cursoRepository, _notifier);

			var cursoUsuarios = _fixture.CreateMany<UsuarioCurso>(5);

			_cursoUsuarioRepository.ObterTodosComIncludeAsync(Arg.Any<Expression<Func<UsuarioCurso, bool>>>()).Returns(cursoUsuarios);

			await Assert.ThrowsAsync<InformacaoException>(() => cursoUsuarioService.ObterCursosDeUsuarioPorIdAsync(Guid.NewGuid()));
		}

		[Fact(DisplayName = "ObterTodosUsuarioCursoPorExpressao_ComUsuarioCursoInvalido_DeveLancarExcecao")]
		public async Task ObterTodosUsuarioCursoPorExpressao_ComUsuarioCursoInvalido_DeveLancarExcecao()
		{
			var cursoUsuarioService = new UsuarioCursoService(_mapper, _cursoUsuarioRepository, _usuarioRepository, _cursoRepository, _notifier);

			var curso = _fixture.Create<Curso>();
			var usuario = _fixture.Create<Usuario>();

			var cursoUsuarios = _fixture.CreateMany<UsuarioCurso>(0);

			_usuarioRepository.ObterPorIdAsync(Arg.Any<Guid>()).Returns(usuario);
			_cursoRepository.ObterPorIdAsync(Arg.Any<Guid>()).Returns(curso);
			_cursoUsuarioRepository.ObterTodosComIncludeAsync(Arg.Any<Expression<Func<UsuarioCurso, bool>>>()).Returns(cursoUsuarios);

			await Assert.ThrowsAsync<InformacaoException>(() => cursoUsuarioService.ObterCursosDeUsuarioPorIdAsync(Guid.NewGuid()));
		}
	}
}
