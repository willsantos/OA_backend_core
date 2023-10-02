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

        public AulaControllerTest()
        {
            _mapper = MapperConfig.Get();
            _fixture = FixtureConfig.GetFixture();
            _service = Substitute.For<IAulaService>();
        }

        [Fact(DisplayName = "Adiciona uma aula")]
        public async Task AulaController_CriaAula_DeveCriar()
        {
			//Arrange
            var aulaController = new AulaController(_service);

            var aulaRequest = new AulaRequest
            {
                Titulo = "TestEntity",
                Tipo = 0,
                Duracao = 0,
                Ordem = 0,
                CursoId = new Guid("cff4e2f5-f132-4a66-969c-dcc76c5ba585"),
            };
            var entity = _mapper.Map<Aula>(aulaRequest);

			//Act
            _service.CadastrarAulaAsync(aulaRequest).Returns(entity.Id);

            var controllerResult = await aulaController.CadastrarAula(aulaRequest);

			//Assert
            var actionResult = controllerResult.Should().BeOfType<CreatedResult>().Subject;

            actionResult.StatusCode.Should().Be(StatusCodes.Status201Created);
            actionResult.Value.Should().Be(entity.Id);
        }


        [Fact(DisplayName = "Busca todas as aulas")]
        public async Task AulaController_BuscaTodasAulaAsync_DeveBuscarTodas()
        {
			//Arrange
            var aulaController = new AulaController(_service);

            var entities = _fixture.Create<List<AulaResponse>>();

            int page = 0;
            int rows = 10;

			//Act
            _service.ObterTodasAulasAsync(page, rows).Returns(entities);
            var controllerResult = await aulaController.ObterTodasAulas(page, rows);

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
            var aulaController = new AulaController(_service);

            var entity = _fixture.Create<AulaResponse>();
            Guid id = Guid.NewGuid();

			//Act
            _service.ObterAulaPorIdAsync(id).Returns(entity);
            var controllerResult = await aulaController.ObterAulaPorId(id);

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
            var aulaController = new AulaController(_service);

            Guid id = Guid.NewGuid();
            var request = _fixture.Create<AulaRequestPut>();

			//Act
            var result = await aulaController.EditarAula(id, request);

			//Assert
            result.Should().BeOfType<NoContentResult>();
            await _service.Received().EditarAulaAsync(id, request);
        }

        [Fact(DisplayName = "Exclui aula")]
        public async Task AulaController_DeleteAulaAsync_DeveAtualizar()
        {
			//Arrange
            var aulaController = new AulaController(_service);

            Guid id = Guid.NewGuid();

			//Act
            var result = await aulaController.DeletarAula(id);

			//Assert
            result.Should().BeOfType<NoContentResult>();
            await _service.Received().DeletarAulaAsync(id);
        }
    }
}
