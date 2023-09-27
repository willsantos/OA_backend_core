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
			var controller = new AssinaturaController(_service);
			var assintauraRequest = new AssinaturaRequest
			{
				Status = AssinaturaStatusEnum.Ativa,
				Tipo = AssinaturaTipoEnum.Anual,
				UsuarioId = new Guid("01b01b8f-4b47-4eb9-9183-1c13e96e8393"),
			};
			var entity = _mapper.Map<Assinatura>(assintauraRequest);

		    _service.PostAssinaturaAsync(assintauraRequest).Returns(entity.Id);

			var controllerResult = await controller.PostAssinaturaAsync(assintauraRequest);
			var actionResult = Assert.IsType<CreatedResult>(controllerResult);

			//Assert
			actionResult.StatusCode.Should().Be(StatusCodes.Status201Created);
			actionResult.Value.Should().Be(entity.Id);
		}

	}
}
