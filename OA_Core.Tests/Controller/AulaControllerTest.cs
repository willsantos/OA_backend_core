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

        [Fact(DisplayName = "Adiciona um aula")]
        public async Task CriaAula()
        {
            var aulaController = new AulaController(_service);

            var aulaRequest = new AulaRequest
            {
                Nome = "TestEntity",
                Descricao = "TestEntity",
                Caminho = "TestEntity",
                Tipo = 0,
                Duracao = 0,
                Ordem = 0,
                CursoId = new Guid("cff4e2f5-f132-4a66-969c-dcc76c5ba585"),
            };
            var entity = _mapper.Map<Aula>(aulaRequest);

            _service.PostAulaAsync(aulaRequest).Returns(entity.Id);

            var controllerResult = await aulaController.PostAulaAsync(aulaRequest);

            //var actionResult = Assert.IsType<CreatedResult>(controllerResult);
            var actionResult = controllerResult.Should().BeOfType<CreatedResult>().Subject;

            actionResult.StatusCode.Should().Be(StatusCodes.Status201Created);
            actionResult.Value.Should().Be(entity.Id);
        }


        [Fact(DisplayName = "Busca todos os aulas")]
        public async Task GetAllAulaAsync()
        {
            var aulaController = new AulaController(_service);

            var entities = _fixture.Create<List<AulaResponse>>();

            int page = 0;
            int rows = 10;

            _service.GetAllAulasAsync(page, rows).Returns(entities);

            var controllerResult = await aulaController.GetAllAulaAsync(page, rows);
            controllerResult.Result.Should().BeOfType<OkObjectResult>();

            var okObjectResult = controllerResult.Result.As<OkObjectResult>();
            okObjectResult.Value.Should().BeAssignableTo<PaginationResponse<AulaResponse>>();


            var resultValue = okObjectResult.Value as PaginationResponse<AulaResponse>;
            resultValue.Resultado.Should().BeEquivalentTo(entities);

        }


        [Fact(DisplayName = "Busca aula por ID")]
        public async Task GetAulaByIdAsync()
        {
            var aulaController = new AulaController(_service);

            var entity = _fixture.Create<AulaResponse>();
            Guid id = Guid.NewGuid();

            _service.GetAulaByIdAsync(id).Returns(entity);

            var controllerResult = await aulaController.GetAulaByIdAsync(id);

            controllerResult.Result.Should().BeOfType<OkObjectResult>();

            var okObjectResult = controllerResult.Result.As<OkObjectResult>();
            okObjectResult.Value.Should().BeAssignableTo<AulaResponse>();


            var resultValue = okObjectResult.Value as AulaResponse;

            resultValue.Should().BeEquivalentTo(entity);
        }

        [Fact(DisplayName = "Atualiza aula")]
        public async Task PutAulaAsync()
        {
            var aulaController = new AulaController(_service);

            Guid id = Guid.NewGuid();
            var request = _fixture.Create<AulaRequestPut>();

            var result = await aulaController.PutAulaAsync(id, request);
            result.Should().BeOfType<NoContentResult>();

            await _service.Received().PutAulaAsync(id, request);
        }

        [Fact(DisplayName = "Exclui aula")]
        public async Task DeleteAulaAsync()
        {
            var aulaController = new AulaController(_service);

            Guid id = Guid.NewGuid();

            var result = await aulaController.DeleteAulaAsync(id);
            result.Should().BeOfType<NoContentResult>();


            await _service.Received().DeleteAulaAsync(id);
        }
    }
}
