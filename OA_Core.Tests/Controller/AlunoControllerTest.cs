using AutoFixture;
using AutoMapper;
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
		public async Task CriarAluno()
		{
			var alunoController = new AlunoController(_service);

			var alunoRequest = _fixture.Create<AlunoRequest>();
			
			var entity = _mapper.Map<Aluno>(alunoRequest);

			_service.PostAlunoAsync(alunoRequest).Returns(entity.Id);

			var controllerResult = await alunoController.PostAlunoAsync(alunoRequest);
			var actionResult = Assert.IsType<ActionResult<Guid>>(controllerResult);
			var createdAtRouteResult = Assert.IsType<CreatedAtRouteResult>(actionResult.Result);


			Assert.Equal(StatusCodes.Status201Created, createdAtRouteResult.StatusCode);
			Assert.Equal(entity.Id, createdAtRouteResult.Value);
		}

		[Fact(DisplayName = "Busca todos os alunos")]
		public async Task GetAllAlunoAsync()
		{
			var alunoController = new AlunoController(_service);

			var entities = _fixture.Create<List<AlunoResponse>>();

			int page = 0;
			int rows = 10;

			_service.GetAllAlunosAsync(page, rows).Returns(entities);

			var controllerResult = await alunoController.GetAllAlunosAsync(page, rows);

			Assert.IsType<OkObjectResult>(controllerResult.Result);

			var resultValue = (controllerResult.Result as OkObjectResult).Value as PaginationResponse<AlunoResponse>;

			Assert.Equal(entities, resultValue.Resultado);
		}

		[Fact(DisplayName = "Busca aluno por id")]
		public async Task GetAlunoByIdAsync()
		{
			var alunoController = new AlunoController(_service);

			var entitiy = _fixture.Create<AlunoResponse>();
			Guid id = Guid.NewGuid();

			_service.GetAlunoByIdAsync(id).Returns(entitiy);

			var controllerResult = await alunoController.GetAlunoByIdAsync(id);

			Assert.IsType<OkObjectResult>(controllerResult.Result);

			var resultValue = (controllerResult.Result as OkObjectResult).Value as AlunoResponse;

			Assert.Equal(entitiy, resultValue);
		}

		[Fact(DisplayName = "Atualiza aluno")]
		public async Task PutAlunoAsync()
		{
			var alunoController = new AlunoController(_service);

			var request = _fixture.Create<AlunoRequestPut>();
			Guid id = Guid.NewGuid();

			var response = await alunoController.PutAlunoAsync(id, request);
			await _service.Received().PutAlunoAsync(id, request);

			var objectResult = Assert.IsType<NoContentResult>(response);
			Assert.Equal(StatusCodes.Status204NoContent, objectResult.StatusCode);
		}

		[Fact(DisplayName = "Deleta aluno")]
		public async Task DeleteAlunoAsync()
		{
			var alunoController = new AlunoController(_service);
			Guid id = Guid.NewGuid();

			var response = await alunoController.DeleteAlunoAsync(id);
			await _service.Received().DeleteAlunoAsync(id);

			var objectResult = Assert.IsType<NoContentResult>(response);
			Assert.Equal(StatusCodes.Status204NoContent, objectResult.StatusCode);
		}
	}
}
