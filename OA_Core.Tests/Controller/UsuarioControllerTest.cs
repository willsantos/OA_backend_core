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
    [Trait("Controller", "Usuario Controller Test")]
    public class UsuarioControllerTest
    {
        private readonly Fixture _fixture;
        private readonly IUsuarioService _cursoSevice;
		private readonly IUsuarioCursoService _usuarioCursoService;
		private readonly IMapper _mapper;

        public UsuarioControllerTest()
        {
            _mapper = MapperConfig.Get();
            _fixture = FixtureConfig.GetFixture();
            _cursoSevice = Substitute.For<IUsuarioService>();
			_usuarioCursoService = Substitute.For<IUsuarioCursoService>();
        }


		[Fact(DisplayName = "Adiciona um UsuarioCurso")]
		public async Task CriaUsuarioCurso()
		{
			var usuarioController = new UsuarioController(_cursoSevice, _usuarioCursoService);

			var usuarioRequest = new UsuarioCursoRequest
			{
				UsuarioId = new Guid(),
				CursoId = new Guid(),
				Progresso = 0,
				Status = 0,
			};

			var entity = _mapper.Map<UsuarioCurso>(usuarioRequest);

			_usuarioCursoService.PostUsuarioCursoAsync(usuarioRequest).Returns(entity.Id);

			var controllerResult = await usuarioController.PostUsuarioCursoAsync(usuarioRequest);
			var actionResult = Assert.IsType<CreatedResult>(controllerResult);

			Assert.Equal(StatusCodes.Status201Created, actionResult.StatusCode);
			Assert.Equal(entity.Id, actionResult.Value);
		}

		[Fact(DisplayName = "Busca relação usuarioCurso por ID")]
		public async Task GetUsuarioCursoByIdAsync()
		{
			var usuarioController = new UsuarioController(_cursoSevice, _usuarioCursoService);

			var entity = _fixture.Create<List<CursoParaUsuarioResponse>>();
			Guid id = Guid.NewGuid();

			_usuarioCursoService.GetCursoDeUsuarioByIdAsync(id).Returns(entity);

			var controllerResult = await usuarioController.GetCursosByUsuarioIdAsync(id);

			Assert.IsType<OkObjectResult>(controllerResult.Result);

			var resultValue = (controllerResult.Result as OkObjectResult).Value as List<CursoParaUsuarioResponse>;

			Assert.Equal(entity, resultValue);
		}
	}
}
