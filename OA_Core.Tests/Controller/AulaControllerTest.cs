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
    [Trait("Controller", "Aula Controller Test")]
    public class AulaControllerTest
    {
        private readonly Fixture _fixture;
        private readonly IAulaService _service;
        private readonly IMapper _mapper;
		private readonly AulaController _aulaController;


		public AulaControllerTest()
        {
            _mapper = MapperConfig.Get();
            _fixture = FixtureConfig.GetFixture();
            _service = Substitute.For<IAulaService>();
			_aulaController = new AulaController(_service);
		}

		[Fact(DisplayName = "Adiciona uma aula")]
        public async Task AulaController_CriaAula_DeveCriar()
        {
			//Arrange
            var aulaRequest = new AulaRequest
            {
                Titulo = "TestEntity",
                Tipo = 0,
                Duracao = 0,
                Ordem = 0,
                CursoId = new Guid("cff4e2f5-f132-4a66-969c-dcc76c5ba585"),
            };
            var entity = _mapper.Map<AulaOnline>(aulaRequest);

			//Act
            _service.CadastrarAulaAsync(aulaRequest).Returns(entity.Id);

            var controllerResult = await _aulaController.CadastrarAula(aulaRequest);

			//Assert
            var actionResult = controllerResult.Should().BeOfType<CreatedResult>().Subject;

            actionResult.StatusCode.Should().Be(StatusCodes.Status201Created);
            actionResult.Value.Should().Be(entity.Id);
        }


        [Fact(DisplayName = "Busca todas as aulas")]
        public async Task AulaController_BuscaTodasAulaAsync_DeveBuscarTodas()
        {
			//Arrange
            var entities = _fixture.Create<List<AulaResponse>>();

            int page = 0;
            int rows = 10;

			//Act
            _service.ObterTodasAulasAsync(page, rows).Returns(entities);
            var controllerResult = await _aulaController.ObterTodasAulas(page, rows);

			//Assert
            controllerResult.Result.Should().BeOfType<OkObjectResult>();

            var okObjectResult = controllerResult.Result.As<OkObjectResult>();
            okObjectResult.Value.Should().BeAssignableTo<PaginationResponse<AulaResponse>>();
            var resultValue = okObjectResult.Value as PaginationResponse<AulaResponse>;
            resultValue.Resultado.Should().BeEquivalentTo(entities);

        }


        [Fact(DisplayName = "Busca aula por ID")]
        public async Task AulaController_BuscaAulaByIdAsync_DeveBuscarUm()
        {
			//Arrange
            var entity = _fixture.Create<AulaResponse>();
            Guid id = Guid.NewGuid();

			//Act
            _service.ObterAulaPorIdAsync(id).Returns(entity);
            var controllerResult = await _aulaController.ObterAulaPorId(id);

			//Assert
            controllerResult.Result.Should().BeOfType<OkObjectResult>();

            var okObjectResult = controllerResult.Result.As<OkObjectResult>();
            okObjectResult.Value.Should().BeAssignableTo<AulaResponse>();


            var resultValue = okObjectResult.Value as AulaResponse;

            resultValue.Should().BeEquivalentTo(entity);
        }

        [Fact(DisplayName = "Atualiza aula")]
        public async Task AulaController_AtualizaAulaAsync_DeveAtualizar()
        {
			//Arrange
            Guid id = Guid.NewGuid();
            var request = _fixture.Create<AulaRequestPut>();

			//Act
            var result = await _aulaController.EditarAula(id, request);

			//Assert
            result.Should().BeOfType<NoContentResult>();
            await _service.Received().EditarAulaAsync(id, request);
        }

        [Fact(DisplayName = "Exclui aula")]
        public async Task AulaController_DeleteAulaAsync_DeveAtualizar()
        {
			//Arrange
            Guid id = Guid.NewGuid();

			//Act
            var result = await _aulaController.DeletarAula(id);

			//Assert
            result.Should().BeOfType<NoContentResult>();
            await _service.Received().DeletarAulaAsync(id);
        }

		[Fact(DisplayName = "Busca aula por cursoId")]
		public async Task AulaController_BuscaAulasPorCursoId_DeveBuscarAulas()
		{
			// Arrange
			var cursoId = Guid.NewGuid();
			var aulaResponses = _fixture.Create<List<AulaResponse>>();

			// Act
			_service.ObterAulasPorCursoIdAsync(cursoId).Returns(aulaResponses);
			var controllerResult = await _aulaController.ObterAulaPorCursoId(cursoId);

			// Assert
			controllerResult.Result.Should().BeOfType<OkObjectResult>();

			var okObjectResult = controllerResult.Result.As<OkObjectResult>();
			okObjectResult.Value.Should().BeAssignableTo<List<AulaResponse>>();

			var resultValue = okObjectResult.Value as List<AulaResponse>;
			resultValue.Should().BeEquivalentTo(aulaResponses);
		}

		// Teste para EditarOrdemAula
		[Fact(DisplayName = "Edita a ordem de uma aula")]
		public async Task AulaController_EditaOrdemAula_DeveEditarOrdem()
		{
			// Arrange
			var id = Guid.NewGuid();
			var ordemRequest = _fixture.Create<OrdemRequest>();

			// Act
			var result = await _aulaController.EditarOrdemAula(id, ordemRequest);

			// Assert
			result.Should().BeOfType<NoContentResult>();
			await _service.Received().EditarOrdemAulaAsync(id, ordemRequest);
		}

		// Teste para EditarOrdensAulas
		[Fact(DisplayName = "Edita as ordens de aulas de um curso")]
		public async Task AulaController_EditaOrdensAulas_DeveEditarOrdens()
		{
			// Arrange
			var cursoId = Guid.NewGuid();
			var ordensRequest = _fixture.Create<OrdensRequest[]>();

			// Act
			var result = await _aulaController.EditarOrdemAula(cursoId, ordensRequest);

			// Assert
			result.Should().BeOfType<NoContentResult>();
			await _service.Received().EditarOrdensAulasAsync(cursoId, ordensRequest);
		}
	}
}
