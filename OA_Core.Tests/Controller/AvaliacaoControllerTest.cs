using AutoFixture;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using OA_Core.Api.Controllers;
using OA_Core.Domain.Contracts.Request;
using OA_Core.Domain.Entities;
using OA_Core.Domain.Interfaces.Service;
using OA_Core.Tests.Config;
using FluentAssertions;
using FluentAssertions.Common;


namespace OA_Core.Tests.Controller
{
	[Trait("Controller", "Avaliacao Controller Test")]
	public class AvaliacaoControllerTest
	{
		private readonly Fixture _fixture;
		private readonly IAvaliacaoService _avaliacaoSevice;		
		private readonly IMapper _mapper;

		public AvaliacaoControllerTest()
		{
			_mapper = MapperConfig.Get();
			_fixture = FixtureConfig.GetFixture();
			_avaliacaoSevice = Substitute.For<IAvaliacaoService>();			
		}
		[Fact(DisplayName = "Adiciona uma avaliacao")]
		public async Task AvaliacaoController_CriaAvaliacao_DeveCriar()
		{
			var avaliacaoController = new AvaliacaoController(_avaliacaoSevice);

			var avaliacaoRequest = _fixture.Create<AvaliacaoRequest>();

			var entity = _mapper.Map<Avaliacao>(avaliacaoRequest);

			_avaliacaoSevice.CadastrarAvaliacaoAsync(avaliacaoRequest).Returns(entity.Id);

			var controllerResult = await avaliacaoController.CadastrarAvaliacao(avaliacaoRequest);
			var actionResult = Assert.IsType<ActionResult<Guid>>(controllerResult);
			var createdAtRouteResult = Assert.IsType<CreatedResult>(actionResult.Result);


			Assert.Equal(StatusCodes.Status201Created, createdAtRouteResult.StatusCode);
			Assert.Equal(entity.Id, createdAtRouteResult.Value);		
			
		}
	}
}
