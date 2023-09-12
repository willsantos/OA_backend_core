
using AutoFixture;
using AutoMapper;
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
		public async Task CriaProfessor()
		{
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

			_service.PostProfessorAsync(professorRequest).Returns(entity.Id);

			var controllerResult = await professorController.PostProfessorAsync(professorRequest);
			var actionResult = Assert.IsType<CreatedResult>(controllerResult);


			Assert.Equal(StatusCodes.Status201Created, actionResult.StatusCode);
			Assert.Equal(entity.Id, actionResult.Value);
		}
		[Fact(DisplayName = "Busca todos os professores com sucesso")]
		public async Task GetAllProfessorAsync()
		{
			var professorController = new ProfessorController(_service);

			var entities = _fixture.Create<List<ProfessorResponse>>();

			int page = 0;
			int rows = 10;

			_service.GetAllProfessoresAsync(page, rows).Returns(entities);

			var controllerResult = await professorController.GetAllProfessorAsync(page, rows);

			Assert.IsType<OkObjectResult>(controllerResult.Result);

			var resultValue = (controllerResult.Result as OkObjectResult).Value as PaginationResponse<ProfessorResponse>;

			Assert.Equal(entities, resultValue.Resultado);
		}

		[Fact(DisplayName = "Busca professor por ID com sucesso")]
		public async Task GetProfessorByIdAsync()
		{
			var professorController = new ProfessorController(_service);

			var entity = _fixture.Create<ProfessorResponse>();
			Guid id = Guid.NewGuid();

			_service.GetProfessorByIdAsync(id).Returns(entity);

			var controllerResult = await professorController.GetProfessorByIdAsync(id);

			Assert.IsType<OkObjectResult>(controllerResult.Result);

			var resultValue = (controllerResult.Result as OkObjectResult).Value as ProfessorResponse;

			Assert.Equal(entity, resultValue);
		}

		[Fact(DisplayName = "Atualiza Professor com sucesso")]
		public async Task PutProfessorAsync()
		{
			var cursoController = new ProfessorController(_service);

			Guid id = Guid.NewGuid();
			var request = _fixture.Create<ProfessorRequestPut>();

			await cursoController.PutProfessorAsync(id, request);

			await _service.Received().PutProfessorAsync(id, request);
		}

		[Fact(DisplayName = "Exclui Professor com sucesso")]
		public async Task DeleteProfessorAsync()
		{
			var cursoController = new ProfessorController(_service);

			Guid id = Guid.NewGuid();

			await cursoController.DeleteProfessorAsync(id);

			await _service.Received().DeleteProfessorAsync(id);
		}		
	}
	
}
	
