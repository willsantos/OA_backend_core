using AutoFixture;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using OA_Core.Domain.Contracts.Request;
using OA_Core.Domain.Contracts.Response;
using OA_Core.Domain.Entities;
using OA_Core.Domain.Interfaces.Notifications;
using OA_Core.Domain.Interfaces.Repository;
using OA_Core.Service;
using OA_Core.Tests.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

		[Fact(DisplayName ="Cria usuário válido")]
		public async Task UsuarioService_CriaUsuario_DeveCadastrar()
		{

			//Arrange
			var mockUsuarioRepository = Substitute.For<IUsuarioRepository>();
			var usuarioService = new UsuarioService(mockUsuarioRepository, _notificador, _mapper);
			var usuarioRequest = _mapper.Map<UsuarioRequest>(_usuarioFixture.GerarUsuario());

			//Act
			mockUsuarioRepository.AddAsync(Arg.Any<Usuario>()).Returns(Task.CompletedTask);
			var resultado = await usuarioService.PostUsuarioAsync(usuarioRequest);

			//Assert
			resultado.Should().NotBe(Guid.Empty,"Guid não pode ser nula");
		}

		[Theory(DisplayName="Obtém todos os usuários")]
		[InlineData(1,20)]
		[InlineData(1,1)]
		[InlineData(1,100)]
		[InlineData(2,10)]
		[InlineData(2,20)]
		[InlineData(4,5)]
		public async Task UsuarioService_ObtemUsuario_DeveRetornarLista(int pagina, int linhas)
		{

			//Arrange
			var quantidadeResultados = pagina * linhas;
			var mockUsuarioRepository = Substitute.For<IUsuarioRepository>();
			var usuarioService = new UsuarioService(mockUsuarioRepository, _notificador, _mapper);
			var usuario = _usuarioFixture.GerarUsuarios(quantidadeResultados,true);
			mockUsuarioRepository.ListPaginationAsync(Arg.Any<int>(), Arg.Any<int>()).Returns(usuario);
			//Act
			var resultado = await usuarioService.GetAllUsuariosAsync(pagina,linhas);
			//Assert
			resultado.Should().HaveCount(quantidadeResultados);
		}

		[Fact(DisplayName ="Obtém um usuario pelo Id")]
		public async Task UsuarioService_ObtemUsuario_DeveRetornarUmUsuario()
		{

			//Arrange
			var mockUsuarioRepository = Substitute.For<IUsuarioRepository>();
			var usuarioService = new UsuarioService(mockUsuarioRepository, _notificador, _mapper);
			var usuario = _usuarioFixture.GerarUsuario();
			var usuarioResponse = _mapper.Map<UsuarioResponse>(usuario);
			//Act
			mockUsuarioRepository.FindAsync(Arg.Any<Guid>()).Returns(usuario);
			var resultado = await usuarioService.GetUsuarioByIdAsync(usuario.Id);
			//Assert
			resultado.Should().BeEquivalentTo(usuarioResponse);
		}

		[Fact(DisplayName ="Atualiza um usuario")]
		public async Task UsuarioService_AtulizaUsuario_DeveAtualizar()
		{

			//Arrange
			var mockUsuarioRepository = Substitute.For<IUsuarioRepository>();
			var usuarioService = new UsuarioService(mockUsuarioRepository, _notificador, _mapper);
			var usuario = _usuarioFixture.GerarUsuario();
			var usuarioRequest = _usuarioFixture.GerarUsuarioRequest();

			//Act
			mockUsuarioRepository.FindAsync(Arg.Any<Guid>()).Returns(usuario);
			await usuarioService.PutUsuarioAsync(usuario.Id, usuarioRequest);

			//Assert
			await mockUsuarioRepository.Received().EditAsync(Arg.Is<Usuario>(u => u.Nome == usuarioRequest.Nome));
		}

		[Fact(DisplayName = "Deleta um Usuario Válido")]
		public async Task UsuarioService_DeletaUsuario_DeveDeletar()
		{

			//Arrange
			var mockUsuarioRepository = Substitute.For<IUsuarioRepository>();
			var usuarioService = new UsuarioService(mockUsuarioRepository, _notificador, _mapper);
			var usuario = _usuarioFixture.GerarUsuario();

			//Act
			mockUsuarioRepository.FindAsync(Arg.Any<Guid>()).Returns(usuario);
			await usuarioService.DeleteUsuarioAsync(usuario.Id);
			//Assert
			await mockUsuarioRepository.Received().RemoveAsync(usuario);

		}
	}
}
