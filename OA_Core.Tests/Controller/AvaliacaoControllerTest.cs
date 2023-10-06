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
using OA_Core.Domain.Contracts.Response;

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
			//Arrange
			var avaliacaoController = new AvaliacaoController(_avaliacaoSevice);

			var avaliacaoRequest = _fixture.Create<AvaliacaoRequest>();

			var entity = _mapper.Map<Avaliacao>(avaliacaoRequest);

			//Act
			_avaliacaoSevice.CadastrarAvaliacaoAsync(avaliacaoRequest).Returns(entity.Id);
			
			var controllerResult = await avaliacaoController.CadastrarAvaliacao(avaliacaoRequest);
			//Assert
			var actionResult = Assert.IsType<ActionResult<Guid>>(controllerResult);
			var createdAtRouteResult = Assert.IsType<CreatedResult>(actionResult.Result);


			Assert.Equal(StatusCodes.Status201Created, createdAtRouteResult.StatusCode);
			Assert.Equal(entity.Id, createdAtRouteResult.Value);		
			
		}

		[Fact(DisplayName = "Inicia uma avaliacao")]
		public async Task AvaliacaoController_IniciaAvaliacao_DeveCriarAvaliacaoUsuario()
		{
			//Arrange
			var avaliacaoController = new AvaliacaoController(_avaliacaoSevice);

			var avaliacaoRequest = _fixture.Create<AvaliacaoUsuarioRequest>();			

			//Act
			await _avaliacaoSevice.IniciarAvaliacaoAsync(avaliacaoRequest);
			//Assert
			var controllerResult = await avaliacaoController.IniciarAvaliacaoUsuario(avaliacaoRequest);
			var actionResult = Assert.IsType<NoContentResult>(controllerResult);
			var createdAtRouteResult = Assert.IsType<NoContentResult>(actionResult);


			Assert.Equal(StatusCodes.Status204NoContent, createdAtRouteResult.StatusCode);	
		}

		[Fact(DisplayName = "Encerra uma avaliacao")]
		public async Task AvaliacaoController_EncerrarAvaliacao_DeveEncerrarAvaliacaoUsuario()
		{
			//Arrange
			var avaliacaoController = new AvaliacaoController(_avaliacaoSevice);

			var avaliacaoRequest = _fixture.Create<AvaliacaoUsuarioRequest>();		

			//Act
			await _avaliacaoSevice.EncerrarAvaliacaoAsync(avaliacaoRequest);
			
			var controllerResult = await avaliacaoController.EncerrarAvaliacaoUsuario(avaliacaoRequest);

			//Assert
			var actionResult = Assert.IsType<NoContentResult>(controllerResult);
			var createdAtRouteResult = Assert.IsType<NoContentResult>(actionResult);


			Assert.Equal(StatusCodes.Status204NoContent, createdAtRouteResult.StatusCode);
		}

		[Fact(DisplayName = "Atualiza avaliar")]
		public async Task AvaliacaoController_AtualizarAavaliacaoAsync_DeveAtualizar()
		{
			//Arrange
			var avaliacaoController = new AvaliacaoController(_avaliacaoSevice);

			var request = _fixture.Create<AvaliacaoRequest>();
			Guid id = Guid.NewGuid();

			//Act
			var response = await avaliacaoController.EditarAvaliacao(request, id);

			//Assert
			await _avaliacaoSevice.Received().EditarAvaliacaoAsync(id, request);

			var objectResult = Assert.IsType<OkObjectResult>(response);
			Assert.Equal(StatusCodes.Status200OK, objectResult.StatusCode);
		}

		[Fact(DisplayName = "Busca avaliacao por id")]
		public async Task AvaliacaoController_BuscaAvaliacaoPorIdAsync_DeveBuscarUm()
		{
			//Arrange
			var avaliacaoController = new AvaliacaoController(_avaliacaoSevice);

			var entity = _fixture.Create<AvaliacaoResponse>();
			Guid id = Guid.NewGuid();

			//Act
			_avaliacaoSevice.ObterAvaliacaoPorIdAsync(id).Returns(entity);

			var controllerResult = await avaliacaoController.ObterAvaliacaoPorId(id);

			//Assert
			controllerResult.Result.Should().BeOfType<OkObjectResult>();

			var resultValue = (controllerResult.Result as OkObjectResult).Value as AvaliacaoResponse;

			resultValue.Should().BeEquivalentTo(entity);
		}

		[Fact(DisplayName = "Deleta avaliacao")]
		public async Task AvaliacaoController_DeleteAvaliacaoAsync_DeveDeletar()
		{
			//Arrange
			var avaliacaoController = new AvaliacaoController(_avaliacaoSevice);
			Guid id = Guid.NewGuid();
			var response = await avaliacaoController.DeletarAvaliacao(id);

			//Assert
			await _avaliacaoSevice.Received().DeletarAvaliacaoAsync(id);

			response.Should().BeOfType<NoContentResult>();
			(response as NoContentResult).StatusCode.Should().Be(StatusCodes.Status204NoContent);
		}
		//Deletar Avaliacao
		//Ativar DesativarAvaliacao
		//Obter Todos
	}
}
