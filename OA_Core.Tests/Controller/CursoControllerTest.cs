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
using OA_Core.Repository.Repositories;
using OA_Core.Service;
using OA_Core.Tests.Config;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_Core.Tests.Controller
{
    [Trait("Controller", "Curso Controller Test")]
    public class CursoControllerTest
    {
        private readonly Fixture _fixture;
        private readonly ICursoService _cursoSevice;
		private readonly ICursoProfessorService _cursoProfessorService;
		private readonly IMapper _mapper;

        public CursoControllerTest()
        {
            _mapper = MapperConfig.Get();
            _fixture = FixtureConfig.GetFixture();
            _cursoSevice = Substitute.For<ICursoService>();
			_cursoProfessorService = Substitute.For<ICursoProfessorService>();
        }

        [Fact(DisplayName = "Adiciona um curso")]
        public async Task CriaCurso()
        {
            var cursoController = new CursoController(_cursoSevice, _cursoProfessorService);

            var cursoRequest = new CursoRequest
            {
                Nome = "TestEntity",
                Descricao = "TestEntity",
                Categoria = "TestEntity",
                PreRequisito = "TestEntity",
                Preco = 100,
                ProfessorId = new Guid("cff4e2f5-f132-4a66-969c-dcc76c5ba585"),
            };
            var entity = _mapper.Map<Curso>(cursoRequest);

            _cursoSevice.PostCursoAsync(cursoRequest).Returns(entity.Id);

            var controllerResult = await cursoController.PostCursoAsync(cursoRequest);
            var actionResult = Assert.IsType<CreatedResult>(controllerResult);


            Assert.Equal(StatusCodes.Status201Created, actionResult.StatusCode);
            Assert.Equal(entity.Id, actionResult.Value);
        }


        [Fact(DisplayName = "Busca todos os cursos")]
        public async Task GetAllCursoAsync()
        {
            var cursoController = new CursoController(_cursoSevice, _cursoProfessorService);

            var entities = _fixture.Create<List<CursoResponse>>();

            int page = 0;
            int rows = 10;

            _cursoSevice.GetAllCursosAsync(page, rows).Returns(entities);

            var controllerResult = await cursoController.GetAllCursoAsync(page, rows);

            Assert.IsType<OkObjectResult>(controllerResult.Result);

            var resultValue = (controllerResult.Result as OkObjectResult).Value as PaginationResponse<CursoResponse>;

            Assert.Equal(entities, resultValue.Resultado);
        }


        [Fact(DisplayName = "Busca curso por ID")]
        public async Task GetCursoByIdAsync()
        {
            var cursoController = new CursoController(_cursoSevice, _cursoProfessorService);

            var entity = _fixture.Create<CursoResponse>();
            Guid id = Guid.NewGuid();

            _cursoSevice.GetCursoByIdAsync(id).Returns(entity);

            var controllerResult = await cursoController.GetCursoByIdAsync(id);

            Assert.IsType<OkObjectResult>(controllerResult.Result);

            var resultValue = (controllerResult.Result as OkObjectResult).Value as CursoResponse;

            Assert.Equal(entity, resultValue);
        }


		[Fact(DisplayName = "Atualiza curso")]
        public async Task PutCursoAsync()
        {
            var cursoController = new CursoController(_cursoSevice, _cursoProfessorService);

            Guid id = Guid.NewGuid();
            var request = _fixture.Create<CursoRequestPut>();

            await cursoController.PutCursoAsync(id, request);

            await _cursoSevice.Received().PutCursoAsync(id, request);
        }

        [Fact(DisplayName = "Exclui curso")]
        public async Task DeleteCursoAsync()
        {
            var cursoController = new CursoController(_cursoSevice, _cursoProfessorService);

            Guid id = Guid.NewGuid();

            await cursoController.DeleteCursoAsync(id);

            await _cursoSevice.Received().DeleteCursoAsync(id);
        }

		[Fact(DisplayName = "Adiciona um cursoProfessor")]
		public async Task CriaCursoProfessor()
		{
			var cursoController = new CursoController(_cursoSevice, _cursoProfessorService);

			var cursoRequest = new CursoProfessorRequest
			{
				ProfessorId = new Guid(),
				Responsavel = true
			};
			var entity = _mapper.Map<CursoProfessor>(cursoRequest);
			entity.CursoId = new Guid();

			_cursoProfessorService.PostCursoProfessorAsync(cursoRequest, entity.CursoId).Returns(entity.Id);

			var controllerResult = await cursoController.PostProfessorToCursoAsync(cursoRequest, entity.CursoId);
			var actionResult = Assert.IsType<CreatedResult>(controllerResult);


			Assert.Equal(StatusCodes.Status201Created, actionResult.StatusCode);
			Assert.Equal(entity.Id, actionResult.Value);
		}

		[Fact(DisplayName = "Busca relação cursoProfessor por ID")]
		public async Task GetCursoProfessorByIdAsync()
		{
			var cursoController = new CursoController(_cursoSevice, _cursoProfessorService);

			var entity = _fixture.Create<List<ProfessorResponseComResponsavel>>();
			Guid id = Guid.NewGuid();

			_cursoProfessorService.GetProfessorDeCursoByIdAsync(id).Returns(entity);

			var controllerResult = await cursoController.GetProfessoresByCursoIdAsync(id);

			Assert.IsType<OkObjectResult>(controllerResult.Result);

			var resultValue = (controllerResult.Result as OkObjectResult).Value as List<ProfessorResponseComResponsavel>;

			Assert.Equal(entity, resultValue);
		}

		[Fact(DisplayName = "Exclui cursoProfessor")]
		public async Task DeleteCursoProfessorAsync()
		{
			var cursoController = new CursoController(_cursoSevice, _cursoProfessorService);

			Guid cursoId = Guid.NewGuid();
			Guid professorId = Guid.NewGuid();


			await cursoController.DeleteProfessorFromCursoAsync(cursoId, professorId);

			await _cursoProfessorService.Received().DeleteCursoProfessorAsync(cursoId, professorId);
		}
	}
}
