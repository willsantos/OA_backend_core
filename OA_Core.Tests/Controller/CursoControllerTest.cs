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
        public async Task CursoController_CriaCurso_DeveCriar()
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

            _cursoSevice.CadastrarCursoAsync(cursoRequest).Returns(entity.Id);

            var controllerResult = await cursoController.CadastrarCruso(cursoRequest);
			

			var createdResult = controllerResult as CreatedResult;

			createdResult.StatusCode.Should().Be(StatusCodes.Status201Created);
			createdResult.Value.Should().Be(entity.Id);
		}


        [Fact(DisplayName = "Busca todos os cursos")]
        public async Task CursoController_BuscarTodosCursosAsync_DeveBuscarTodos()
        {
			//Arrange
            var cursoController = new CursoController(_cursoSevice, _cursoProfessorService);

            var entities = _fixture.Create<List<CursoResponse>>();

            int page = 0;
            int rows = 10;

			//Act
            _cursoSevice.ObterTodosCursosAsync(page, rows).Returns(entities);

            var controllerResult = await cursoController.ObterTodosCursos(page, rows);

			//Assert
			controllerResult.Result.Should().BeOfType<OkObjectResult>();

			var resultValue = (controllerResult.Result as OkObjectResult).Value as PaginationResponse<CursoResponse>;

			resultValue.Resultado.Should().BeEquivalentTo(entities);
		}


        [Fact(DisplayName = "Busca curso por ID")]
        public async Task CursoController_BuscaCursoByIdAsync_DeveBuscarUm()
        {
			//Arrange
            var cursoController = new CursoController(_cursoSevice, _cursoProfessorService);

            var entity = _fixture.Create<CursoResponse>();
            Guid id = Guid.NewGuid();

			//Act
            _cursoSevice.ObterCursoPorIdAsync(id).Returns(entity);

            var controllerResult = await cursoController.ObterCursoPorId(id);

			//Assert
			controllerResult.Result.Should().BeOfType<OkObjectResult>();

			var resultValue = (controllerResult.Result as OkObjectResult).Value as CursoResponse;

			resultValue.Should().BeEquivalentTo(entity);
		}


		[Fact(DisplayName = "Atualiza curso")]
        public async Task CursoController_AtualizaCursoAsync_DeveAtualizar()
        {
			//Arrange
            var cursoController = new CursoController(_cursoSevice, _cursoProfessorService);

            Guid id = Guid.NewGuid();
            var request = _fixture.Create<CursoRequestPut>();

			//Act
            await cursoController.EditarCurso(id, request);

			//Assert
            await _cursoSevice.Received().EditarCursoAsync(id, request);
        }

        [Fact(DisplayName = "Exclui curso")]
        public async Task CursoController_DeleteCursoAsync_DeveDeletar()
        {
			//Arrange
            var cursoController = new CursoController(_cursoSevice, _cursoProfessorService);

            Guid id = Guid.NewGuid();

			//Act
            await cursoController.DeletarCurso(id);

			//Assert
            await _cursoSevice.Received().DeletarCursoAsync(id);
        }

		[Fact(DisplayName = "Adiciona um cursoProfessor")]
		public async Task CursoController_CriaCursoProfessor_DeveCriar()
		{
			//Arrange
			var cursoController = new CursoController(_cursoSevice, _cursoProfessorService);

			var cursoRequest = new CursoProfessorRequest
			{
				ProfessorId = new Guid(),
				Responsavel = true
			};
			var entity = _mapper.Map<CursoProfessor>(cursoRequest);
			entity.CursoId = new Guid();

			//Act
			_cursoProfessorService.CadastrarCursoProfessorAsync(cursoRequest, entity.CursoId).Returns(entity.Id);
			var controllerResult = await cursoController.CadastrarProfessorACurso(cursoRequest, entity.CursoId);

			//Assert
			controllerResult.Should().BeOfType<CreatedResult>();

			var createdResult = controllerResult as CreatedResult;

			createdResult.StatusCode.Should().Be(StatusCodes.Status201Created);
			createdResult.Value.Should().Be(entity.Id);
		}

		[Fact(DisplayName = "Busca relação cursoProfessor por ID")]
		public async Task CursoController_BuscaCursoProfessorByIdAsync_DeveBuscar()
		{
			//Arrange
			var cursoController = new CursoController(_cursoSevice, _cursoProfessorService);

			var entity = _fixture.Create<List<ProfessorResponseComResponsavel>>();
			Guid id = Guid.NewGuid();

			//Act
			_cursoProfessorService.ObterProfessoresDeCursoPorIdAsync(id).Returns(entity);

			var controllerResult = await cursoController.ObterProfessoresDeCurso(id);

			//Assert
			controllerResult.Result.Should().BeOfType<OkObjectResult>();

			var resultValue = (controllerResult.Result as OkObjectResult).Value as List<ProfessorResponseComResponsavel>;

			resultValue.Should().BeEquivalentTo(entity);
		}

		[Fact(DisplayName = "Exclui cursoProfessor")]
		public async Task CursoController_DeleteCursoProfessorAsync_DeveDeletar()
		{
			//Arrange
			var cursoController = new CursoController(_cursoSevice, _cursoProfessorService);

			Guid cursoId = Guid.NewGuid();
			Guid professorId = Guid.NewGuid();

			//Act
			await cursoController.DeletarProfessorDeCurso(cursoId, professorId);

			//Assert
			await _cursoProfessorService.Received().DeletarCursoProfessorAsync(cursoId, professorId);
		}
	}
}
