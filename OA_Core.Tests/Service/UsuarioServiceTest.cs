using AutoFixture;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using OA_Core.Domain.Contracts.Request;
using OA_Core.Domain.Contracts.Response;
using OA_Core.Domain.Entities;
using OA_Core.Domain.Exceptions;
using OA_Core.Domain.Interfaces.Notifications;
using OA_Core.Domain.Interfaces.Repository;
using OA_Core.Service;
using OA_Core.Tests.Config;

namespace OA_Core.Tests.Service
{
	[Collection(nameof(UsuarioBogusCollection))]
	[Trait("Usuario", "Service")]
	public class UsuarioServiceTest
	{
		private readonly IMapper _mapper;
		private readonly Fixture _fixture;
		private readonly INotificador _notificador;
		private readonly UsuarioFixture _usuarioFixture;

		public UsuarioServiceTest(UsuarioFixture usuarioFixture)
		{
			_mapper = MapperConfig.Get();
			_fixture = FixtureConfig.GetFixture();
			_notificador = Substitute.For<INotificador>();
			_usuarioFixture = usuarioFixture;
		}

		[Fact(DisplayName = "Cria usuário válido")]
		public async Task UsuarioService_CriaUsuario_DeveCadastrar()
		{

			//Arrange
			var mockUsuarioRepository = Substitute.For<IUsuarioRepository>();
			var usuarioService = new UsuarioService(mockUsuarioRepository, _notificador, _mapper);
			var usuarioRequest = _mapper.Map<UsuarioRequest>(UsuarioFixture.GerarUsuario());

			//Act
			mockUsuarioRepository.AdicionarAsync(Arg.Any<Usuario>()).Returns(Task.CompletedTask);
			var resultado = await usuarioService.PostUsuarioAsync(usuarioRequest);

			//Assert
			resultado.Should().NotBe(Guid.Empty, "Guid não pode ser nula");
		}

		[Theory(DisplayName = "Obtém todos os usuários")]
		[InlineData(1, 20)]
		[InlineData(1, 1)]
		[InlineData(1, 100)]
		[InlineData(3, 5)]
		[InlineData(2, 20)]
		[InlineData(4, 5)]
		public async Task UsuarioService_ObtemUsuario_DeveRetornarLista(int pagina, int linhas)
		{

			//Arrange
			var mockUsuarioRepository = Substitute.For<IUsuarioRepository>();
			var usuarioService = new UsuarioService(mockUsuarioRepository, _notificador, _mapper);
			var usuario = UsuarioFixture.GerarUsuarios(linhas, true);
			mockUsuarioRepository.ObterTodosAsync(Arg.Any<int>(), Arg.Any<int>()).Returns(usuario);
			//Act
			var resultado = await usuarioService.GetAllUsuariosAsync(pagina, linhas);
			//Assert
			resultado.Should().HaveCount(linhas);
		}

		[Fact(DisplayName = "Obtém um usuario pelo Id")]
		public async Task UsuarioService_ObtemUsuario_DeveRetornarUmUsuario()
		{

			//Arrange
			var mockUsuarioRepository = Substitute.For<IUsuarioRepository>();
			var usuarioService = new UsuarioService(mockUsuarioRepository, _notificador, _mapper);
			var usuario = UsuarioFixture.GerarUsuario();
			var usuarioResponse = _mapper.Map<UsuarioResponse>(usuario);
			//Act
			mockUsuarioRepository.ObterPorIdAsync(Arg.Any<Guid>()).Returns(usuario);
			var resultado = await usuarioService.GetUsuarioByIdAsync(usuario.Id);
			//Assert
			resultado.Should().BeEquivalentTo(usuarioResponse);
		}

		[Fact(DisplayName = "Atualiza um usuario")]
		public async Task UsuarioService_AtulizaUsuario_DeveAtualizar()
		{

			//Arrange
			var mockUsuarioRepository = Substitute.For<IUsuarioRepository>();
			var usuarioService = new UsuarioService(mockUsuarioRepository, _notificador, _mapper);
			var usuario = UsuarioFixture.GerarUsuario();
			var usuarioRequest = UsuarioFixture.GerarUsuarioRequest();

			//Act
			mockUsuarioRepository.ObterPorIdAsync(Arg.Any<Guid>()).Returns(usuario);
			await usuarioService.PutUsuarioAsync(usuario.Id, usuarioRequest);

			//Assert
			await mockUsuarioRepository.Received().EditarAsync(Arg.Is<Usuario>(u => u.Nome == usuarioRequest.Nome));
		}

		[Fact(DisplayName = "Deleta um Usuario Válido")]
		public async Task UsuarioService_DeletaUsuario_DeveDeletar()
		{

			//Arrange
			var mockUsuarioRepository = Substitute.For<IUsuarioRepository>();
			var usuarioService = new UsuarioService(mockUsuarioRepository, _notificador, _mapper);
			var usuario = UsuarioFixture.GerarUsuario();

			//Act
			mockUsuarioRepository.ObterPorIdAsync(Arg.Any<Guid>()).Returns(usuario);
			await usuarioService.DeleteUsuarioAsync(usuario.Id);
			//Assert
			await mockUsuarioRepository.Received().RemoverAsync(usuario);

		}

		[Fact(DisplayName = "Tenta Cadastrar um Usuario inválido")]
		public async Task UsuarioService_CriaUsuario_DeveSerInvalido()
		{

			//Arrange
			var mockUsuarioRepository = Substitute.For<IUsuarioRepository>();
			var usuarioService = new UsuarioService(mockUsuarioRepository, _notificador, _mapper);
			var usuarioRequest = _mapper.Map<UsuarioRequest>(UsuarioFixture.GerarUsuarioInvalido());

			//Act
			mockUsuarioRepository.AdicionarAsync(Arg.Any<Usuario>()).Returns(Task.CompletedTask);
			await usuarioService.PostUsuarioAsync(usuarioRequest);

			//Assert
			_notificador.Received().Handle(
				Arg.Is<FluentValidation.Results.ValidationResult>(
					v => v.Errors.Any(err => err.PropertyName != null)));

		}

		[Fact(DisplayName = "Tenta Atualizar um Usuario não existente")]
		public async Task UsuarioService_AtualizarUsuario_DeveSerInexistente()
		{

			//Arrange
			var mockUsuarioRepository = Substitute.For<IUsuarioRepository>();
			var usuarioService = new UsuarioService(mockUsuarioRepository, _notificador, _mapper);
			var usuarioRequest = _mapper.Map<UsuarioRequest>(UsuarioFixture.GerarUsuario());

			//Act - Assert			
			await Assert.ThrowsAsync<InformacaoException>(() => usuarioService.PutUsuarioAsync(Guid.NewGuid(), usuarioRequest));
		}

		[Fact(DisplayName = "Tenta Atualizar um Usuario inválido")]
		public async Task UsuarioService_AtualizarUsuario_DeveSerInvalido()
		{

			//Arrange
			var mockUsuarioRepository = Substitute.For<IUsuarioRepository>();
			var usuarioService = new UsuarioService(mockUsuarioRepository, _notificador, _mapper);
			var usuario = UsuarioFixture.GerarUsuario();
			var usuarioRequest = _mapper.Map<UsuarioRequest>(UsuarioFixture.GerarUsuarioInvalido());

			//Act
			mockUsuarioRepository.ObterPorIdAsync(Arg.Any<Guid>()).Returns(usuario);
			await usuarioService.PutUsuarioAsync(usuario.Id, usuarioRequest);

			//Assert
			_notificador.Received().Handle(
				Arg.Is<FluentValidation.Results.ValidationResult>(
					v => v.Errors.Any(err => err.PropertyName != null)));

		}

		[Fact(DisplayName = "Tenta obter um usuário pelo Id inválido")]
		public async Task UsuarioService_ObeterUsuario_DeveSerInvalido()
		{

			//Arrange
			var mockUsuarioRepository = Substitute.For<IUsuarioRepository>();
			var usuarioService = new UsuarioService(mockUsuarioRepository, _notificador, _mapper);


			//Act
			//Assert
			await Assert.ThrowsAsync<InformacaoException>(() => usuarioService.GetUsuarioByIdAsync(Guid.NewGuid()));
		}

		[Fact(DisplayName = "Tenta Deletar um usuário pelo Id inválido")]
		public async Task UsuarioService_DeletarUsuario_DeveSerInvalido()
		{

			//Arrange
			var mockUsuarioRepository = Substitute.For<IUsuarioRepository>();
			var usuarioService = new UsuarioService(mockUsuarioRepository, _notificador, _mapper);


			//Act
			//Assert
			await Assert.ThrowsAsync<InformacaoException>(() => usuarioService.DeleteUsuarioAsync(Guid.NewGuid()));
		}


	}
}
