using AutoFixture;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI.Common;
using NSubstitute;
using OA_Core.Api.Controllers;
using OA_Core.Domain.Contracts.Request;
using OA_Core.Domain.Contracts.Response;
using OA_Core.Domain.Entities;
using OA_Core.Domain.Interfaces.Service;
using OA_Core.Tests.Config;

namespace OA_Core.Tests.Controller
{
	[Trait("Controller", "Aluno Controller Test")]
	public class AlunoControllerTest
	{
		private readonly Fixture _fixture;
		private readonly IAlunoService _service;
		private readonly IMapper _mapper;

		public AlunoControllerTest()
		{
			_mapper = MapperConfig.Get();
			_fixture = FixtureConfig.GetFixture();
			_service = Substitute.For<IAlunoService>();
		}

		[Fact(DisplayName = "Adiciona um aluno")]
		public async Task AlunoController_CriarAluno_DeveCriar()
		{
			var alunoController = new AlunoController(_service);

			var alunoRequest = _fixture.Create<AlunoRequest>();
			
			var entity = _mapper.Map<Aluno>(alunoRequest);

			_service.PostAlunoAsync(alunoRequest).Returns(entity.Id);

			var controllerResult = await alunoController.CadastrarAluno(alunoRequest);
			var actionResult = Assert.IsType<ActionResult<Guid>>(controllerResult);
			var createdAtRouteResult = Assert.IsType<CreatedAtRouteResult>(actionResult.Result);


			Assert.Equal(StatusCodes.Status201Created, createdAtRouteResult.StatusCode);
			Assert.Equal(entity.Id, createdAtRouteResult.Value);
		}

		[Fact(DisplayName = "Busca todos os alunos")]
		public async Task AlunoController_BuscarTodosAlunoAsync_DeveBuscarTodos()
		{

			//Arrange
			var alunoController = new AlunoController(_service);

			var entities = _fixture.Create<List<AlunoResponse>>();

			int page = 0;
			int rows = 10;

			//Act
			_service.GetAllAlunosAsync(page, rows).Returns(entities);

			var controllerResult = await alunoController.ObterTodosAlunos(page, rows);

			//Assert
			controllerResult.Result.Should().BeOfType<OkObjectResult>();

			var resultValue = (controllerResult.Result as OkObjectResult).Value as PaginationResponse<AlunoResponse>;

			resultValue.Resultado.Should().BeEquivalentTo(entities);
		}

		[Fact(DisplayName = "Busca aluno por id")]
		public async Task AlunoController_BuscaAlunoPorIdAsync_DeveBuscarUm()
		{
			//Arrange
			var alunoController = new AlunoController(_service);

			var entity = _fixture.Create<AlunoResponse>();
			Guid id = Guid.NewGuid();

			//Act
			_service.GetAlunoByIdAsync(id).Returns(entity);

			var controllerResult = await alunoController.ObterAlunoPorId(id);

			//Assert
			controllerResult.Result.Should().BeOfType<OkObjectResult>();

			var resultValue = (controllerResult.Result as OkObjectResult).Value as AlunoResponse;

			resultValue.Should().BeEquivalentTo(entity);
		}

		[Fact(DisplayName = "Atualiza aluno")]
		public async Task AlunoController_AtualizarAlunoAsync_DeveAtualizar()
		{
			//Arrange
			var alunoController = new AlunoController(_service);

			var request = _fixture.Create<AlunoRequestPut>();
			Guid id = Guid.NewGuid();

			//Act
			var response = await alunoController.EditarAluno(id, request);

			//Assert
			await _service.Received().PutAlunoAsync(id, request);

			var objectResult = Assert.IsType<NoContentResult>(response);
			Assert.Equal(StatusCodes.Status204NoContent, objectResult.StatusCode);
		}

		[Fact(DisplayName = "Deleta aluno")]
		public async Task AlunoController_DeleteAlunoAsync_DeveDeletar()
		{
			//Arrange
			var alunoController = new AlunoController(_service);
			Guid id = Guid.NewGuid();
			var response = await alunoController.DeletarAluno(id);

			//Assert
			await _service.Received().DeleteAlunoAsync(id);

			response.Should().BeOfType<NoContentResult>();
			(response as NoContentResult).StatusCode.Should().Be(StatusCodes.Status204NoContent);
		}
	}
}
