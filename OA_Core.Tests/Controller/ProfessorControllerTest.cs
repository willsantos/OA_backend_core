
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

		[Fact(DisplayName = "Adiciona um novo professor")]
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
	}
	
}
	
