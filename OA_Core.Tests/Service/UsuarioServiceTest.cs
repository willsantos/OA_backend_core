using AutoFixture;
using AutoMapper;
using NSubstitute;
using OA_Core.Domain.Contracts.Request;
using OA_Core.Domain.Entities;
using OA_Core.Domain.Interfaces.Notifications;
using OA_Core.Domain.Interfaces.Repository;
using OA_Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_Core.Tests.Service
{
	public class UsuarioServiceTest
	{
		private readonly IMapper _mapper;
		private readonly Fixture _fixture;
		private readonly INotificador _notificador;

		public UsuarioServiceTest(IMapper mapper, Fixture fixture, INotificador notificador)
		{
			_mapper = mapper;
			_fixture = fixture;
			_notificador = notificador;
		}

		[Fact(DisplayName ="Cria usuário válido")]
		public async Task UsuarioService_CriaUsuario_DeveCadastrar()
		{

			//Arrange
			var mockUsuarioRepository = Substitute.For<IUsuarioRepository>();
			var usuarioService = new UsuarioService(mockUsuarioRepository, _notificador, _mapper);
			var usuarioRequest = _fixture.Create<UsuarioRequest>();

			//Act
			mockUsuarioRepository.AddAsync(Arg.Any<Usuario>()).Returns(Task.CompletedTask);
			var resultado = await usuarioService.PostUsuarioAsync(usuarioRequest);

			//Assert
			
		}
	}
}
