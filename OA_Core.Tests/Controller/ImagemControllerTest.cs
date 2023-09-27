using AutoFixture;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI.Common;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using OA_Core.Api.Controllers;
using OA_Core.Domain.Contracts.Request;
using OA_Core.Domain.Contracts.Response;
using OA_Core.Domain.Entities;
using OA_Core.Domain.Enums;
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
	public class ImagemControllerTest
	{
		private readonly Fixture _fixture;
		private readonly IImagemService _service;
		private readonly IMapper _mapper;

		public ImagemControllerTest()
		{
			_mapper = MapperConfig.Get();
			_fixture = FixtureConfig.GetFixture();
			_service = Substitute.For<IImagemService>();
		}

		[Fact(DisplayName = "Adiciona uma imagem")]
		public async Task UploadImagem()
		{
			var imagemController = new ImagemController(_service);

			var formFile = Substitute.For<IFormFile>();
			formFile.Length.Returns(100); // Set a valid file length
			formFile.FileName.Returns("test_image");
			var tipoimagem = TipoImagem.fotoOutro;


			_service.SaveImageAsync(formFile, tipoimagem).Returns("mock_resultado_sem_problemas");

			var controllerResult = await imagemController.EnviarImagem(formFile, tipoimagem);

			controllerResult.Should().NotBeNull();
			controllerResult.Should().BeAssignableTo<OkObjectResult>();

			var result = controllerResult as OkObjectResult;

			result.StatusCode.Should().Be(StatusCodes.Status200OK);
			result.Value.Should().Be("mock_resultado_sem_problemas");
		}
	}
}
