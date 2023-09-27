
using AutoFixture;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Validations;
using NSubstitute;
using OA_Core.Api.Controllers;
using OA_Core.Domain.Contracts.Request;
using OA_Core.Domain.Contracts.Response;
using OA_Core.Domain.Entities;
using OA_Core.Domain.Interfaces.Service;
using OA_Core.Tests.Config;

namespace OA_Core.Tests.Controller
{
	[Trait("Professor", "Professor Controller Test")]
	public class ProfessorControllerTest
	{
		private readonly Fixture _fixture;
		private readonly IProfessorService _service;
		private readonly IMapper _mapper;

		public ProfessorControllerTest()
		{
			_mapper = MapperConfig.Get();
			_fixture = FixtureConfig.GetFixture();
			_service = Substitute.For<IProfessorService>();
		}

		[Fact(DisplayName = "Adiciona um novo professor com sucesso")]
		public async Task ProfessorController_CriaProfessor_DeveCriar()
		{
			//Arrange
			var professorController = new ProfessorController(_service);

			var professorRequest = new ProfessorRequest
			{
				Formacao = "Formação teste",				
				Experiencia = "Experiencia Teste",
				Foto = "Foto Teste",
				Biografia = "Bio Testes",				
				UsuarioId = new Guid("838e8395-5794-4f81-8d53-813a91485643"),
			};
			var entity = _mapper.Map<Professor>(professorRequest);

			//Act
			_service.CadastrarProfessorAsync(professorRequest).Returns(entity.Id);
			var controllerResult = await professorController.CadastrarProfessor(professorRequest);
			var actionResult = Assert.IsType<CreatedResult>(controllerResult);

			//Assert
			actionResult.StatusCode.Should().Be(StatusCodes.Status201Created);
			actionResult.Value.Should().Be(entity.Id);
		}

		[Fact(DisplayName = "Busca todos os professores com sucesso")]
		public async Task ProfessorController_GetAllProfessorAsync_DeveBuscar()
		{
			//Arrange
			var professorController = new ProfessorController(_service);

			var entities = _fixture.Create<List<ProfessorResponse>>();

			int page = 0;
			int rows = 10;

			//Act
			_service.ObterTodosProfessoresAsync(page, rows).Returns(entities);
			var controllerResult = await professorController.ObterTodosProfessores(page, rows);

			//Assert
			controllerResult.Result.Should().BeOfType<OkObjectResult>();

			var resultValue = ((OkObjectResult)controllerResult.Result).Value as PaginationResponse<ProfessorResponse>;

			resultValue.Resultado.Should().BeEquivalentTo(entities);

		}

		[Fact(DisplayName = "Busca professor por ID com sucesso")]
		public async Task ProfessorController_GetProfessorByIdAsync_DeveBuscarUm()
		{
			//Arrange
			var professorController = new ProfessorController(_service);

			var entity = _fixture.Create<ProfessorResponse>();
			Guid id = Guid.NewGuid();

			//Act
			_service.ObterProfessorPorIdAsync(id).Returns(entity);

			var controllerResult = await professorController.ObterProfessorPorId(id);

			//Assert
			controllerResult.Result.Should().BeOfType<OkObjectResult>();

			var resultValue = (controllerResult.Result as OkObjectResult).Value as ProfessorResponse;

			resultValue.Should().BeEquivalentTo(entity);
		}

		[Fact(DisplayName = "Atualiza Professor com sucesso")]
		public async Task ProfessorController_PutProfessorAsync_DeveAtualizar()
		{
			//Arrange
			var cursoController = new ProfessorController(_service);

			Guid id = Guid.NewGuid();
			var request = _fixture.Create<ProfessorRequestPut>();

			//Act
			await cursoController.EditarProfessor(id, request);

			//Assert
			await _service.Received().EditarProfessorAsync(id, request);
		}

		[Fact(DisplayName = "Exclui Professor com sucesso")]
		public async Task ProfessorController_DeleteProfessorAsync_DeveDeletar()
		{
			//Arrange
			var cursoController = new ProfessorController(_service);

			Guid id = Guid.NewGuid();

			//Act
			await cursoController.DeletarProfessor(id);

			//Assert
			await _service.Received().DeletarProfessorAsync(id);
		}		
	}
	
}
	
