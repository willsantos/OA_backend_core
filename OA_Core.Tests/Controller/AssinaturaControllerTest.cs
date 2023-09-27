using AutoFixture;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using OA_Core.Api.Controllers;
using OA_Core.Domain.Contracts.Request;
using OA_Core.Domain.Entities;
using OA_Core.Domain.Interfaces.Service;
using OA_Core.Domain.Utils;
using OA_Core.Tests.Config;

namespace OA_Core.Tests.Controller
{
	[Trait("Controller", "Assinatura Controller Test")]
	public class AssinaturaControllerTest
	{
		private readonly Fixture _fixture;
		private readonly IAssinaturaService _service;
		private readonly IMapper _mapper;

		public AssinaturaControllerTest()
		{
			_mapper = MapperConfig.Get();
			_fixture = FixtureConfig.GetFixture();
			_service = Substitute.For<IAssinaturaService>();
		}

		[Fact(DisplayName = "Adiciona assinatura")]
		public async Task criarAssinatura()
		{
			//Arrange
			var controller = new AssinaturaController(_service);
			var assintauraRequest = new AssinaturaRequest
			{
				Status = AssinaturaStatusEnum.Ativa,
				Tipo = AssinaturaTipoEnum.Anual,
				UsuarioId = new Guid("01b01b8f-4b47-4eb9-9183-1c13e96e8393"),
			};
			var entity = _mapper.Map<Assinatura>(assintauraRequest);

			//Act
			_service.AdicionarAssinaturaAsync(assintauraRequest).Returns(entity.Id);

			var controllerResult = await controller.AdicionarAssinatura(assintauraRequest);

			//Assert
			var actionResult = Assert.IsType<CreatedResult>(controllerResult);
			
			actionResult.StatusCode.Should().Be(StatusCodes.Status201Created);
			actionResult.Value.Should().Be(entity.Id);
		}

		[Fact(DisplayName = "Cancela assinatura com sucesso")]
		public async Task PutAssinaturaAsync()
		{
			//Arrange
			var cursoController = new AssinaturaController(_service);

			Guid id = Guid.NewGuid();
			var request = _fixture.Create<AssinaturaCancelamentoRequest>();

			//Act
			await cursoController.CancelarAssinatura(id, request);

			//Assert
			await _service.Received().CancelarAssinaturaAsync(id, request);
		}

	}
}
