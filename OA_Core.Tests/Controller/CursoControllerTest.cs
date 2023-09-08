﻿using AutoFixture;
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
        private readonly ICursoService _service;
        private readonly IMapper _mapper;

        public CursoControllerTest()
        {
            _mapper = MapperConfig.Get();
            _fixture = FixtureConfig.GetFixture();
            _service = Substitute.For<ICursoService>();
        }

        [Fact(DisplayName = "Adiciona um curso")]
        public async Task CriaCurso()
        {
            var cursoController = new CursoController(_service);

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

            _service.PostCursoAsync(cursoRequest).Returns(entity.Id);

            var controllerResult = await cursoController.PostCursoAsync(cursoRequest);
            var actionResult = Assert.IsType<CreatedResult>(controllerResult);


            Assert.Equal(StatusCodes.Status201Created, actionResult.StatusCode);
            Assert.Equal(entity.Id, actionResult.Value);
        }


        [Fact(DisplayName = "Busca todos os cursos")]
        public async Task GetAllCursoAsync()
        {
            var cursoController = new CursoController(_service);

            var entities = _fixture.Create<List<CursoResponse>>();

            int page = 0;
            int rows = 10;

            _service.GetAllCursosAsync(page, rows).Returns(entities);

            var controllerResult = await cursoController.GetAllCursoAsync(page, rows);

            Assert.IsType<OkObjectResult>(controllerResult.Result);

            var resultValue = (controllerResult.Result as OkObjectResult).Value as PaginationResponse<CursoResponse>;

            Assert.Equal(entities, resultValue.Resultado);
        }


        [Fact(DisplayName = "Busca curso por ID")]
        public async Task GetCursoByIdAsync()
        {
            var cursoController = new CursoController(_service);

            var entity = _fixture.Create<CursoResponse>();
            Guid id = Guid.NewGuid();

            _service.GetCursoByIdAsync(id).Returns(entity);

            var controllerResult = await cursoController.GetCursoByIdAsync(id);

            Assert.IsType<OkObjectResult>(controllerResult.Result);

            var resultValue = (controllerResult.Result as OkObjectResult).Value as CursoResponse;

            Assert.Equal(entity, resultValue);
        }

        [Fact(DisplayName = "Atualiza curso")]
        public async Task PutCursoAsync()
        {
            var cursoController = new CursoController(_service);

            Guid id = Guid.NewGuid();
            var request = _fixture.Create<CursoRequestPut>();

            await cursoController.PutCursoAsync(id, request);

            await _service.Received().PutCursoAsync(id, request);
        }

        [Fact(DisplayName = "Exclui curso")]
        public async Task DeleteCursoAsync()
        {
            var cursoController = new CursoController(_service);

            Guid id = Guid.NewGuid();

            await cursoController.DeleteCursoAsync(id);

            await _service.Received().DeleteCursoAsync(id);
        }
    }
}