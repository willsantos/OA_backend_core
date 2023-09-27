using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using OA_Core.Domain.Enums;
using OA_Core.Domain.Exceptions;
using OA_Core.Domain.Interfaces.Service;
using OA_Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_Core.Tests.Service
{
	[Trait("Service", "Imagem Service")]

	public class ImagemServiceTests
	{
		[Fact(DisplayName = "Faz um upload valido")]
		public async Task SaveImageAsync_ValidFile_ReturnsFilePath()
		{
			// Arrange
			var hostingEnvironment = Substitute.For<IHostingEnvironment>();
			hostingEnvironment.ContentRootPath.Returns("mocked_content_root_path");

			var formFile = Substitute.For<IFormFile>();
			formFile.Length.Returns(100); // Set a valid file length
			formFile.FileName.Returns("test_image");
			formFile.ContentType.Returns("image/jpg");

			var tipoimagem = TipoImagem.fotoOutro;

			var imagemService = new ImagemService(hostingEnvironment);

			// Act
			var result = await imagemService.SalvarImagemAsync(formFile, tipoimagem);

			var expectedPath = Path.Combine("images", tipoimagem.ToString().ToLower(), "test_image");

			// Assert
			result.Should().NotBeNullOrEmpty();
			result.Should().StartWith(expectedPath);
		}

		[Fact(DisplayName = "Faz um upload invalido sem bytes")]
		public async Task SaveImageAsync_InvalidByteFile_ThrowsException()
		{
			// Arrange
			var hostingEnvironment = Substitute.For<IHostingEnvironment>();
			hostingEnvironment.ContentRootPath.Returns("your_mocked_content_root_path");

			var formFile = Substitute.For<IFormFile>();
			formFile.Length.Returns(0);
			formFile.FileName.Returns("invalid_image.jpg");
			formFile.ContentType.Returns("image/jpg");

			var tipoimagem = TipoImagem.fotoOutro;

			var imagemService = new ImagemService(hostingEnvironment);

			// Act and Assert
			await Assert.ThrowsAsync<InformacaoException>(() => imagemService.SalvarImagemAsync(formFile, tipoimagem));
		}

		[Fact(DisplayName = "Faz um upload com formato de arquivo invalido")]
		public async Task SaveImageAsync_InvalidFormatFile_ThrowsException()
		{
			// Arrange
			var hostingEnvironment = Substitute.For<IHostingEnvironment>();
			hostingEnvironment.ContentRootPath.Returns("your_mocked_content_root_path");

			var formFile = Substitute.For<IFormFile>();
			formFile.Length.Returns(100);
			formFile.FileName.Returns("invalid_image.jpg");
			formFile.ContentType.Returns("text/txt");

			var tipoimagem = TipoImagem.fotoOutro;

			var imagemService = new ImagemService(hostingEnvironment);

			// Act and Assert
			await Assert.ThrowsAsync<InformacaoException>(() => imagemService.SalvarImagemAsync(formFile, tipoimagem));
		}

		[Fact(DisplayName = "Faz um upload invalido Nulo")]
		public async Task SaveImageAsync_NullFile_ThrowsException()
		{
			// Arrange
			var hostingEnvironment = Substitute.For<IHostingEnvironment>();
			hostingEnvironment.ContentRootPath.Returns("your_mocked_content_root_path");

			IFormFile form = null;

			var tipoimagem = TipoImagem.fotoOutro;

			var imagemService = new ImagemService(hostingEnvironment);

			// Act and Assert
			await Assert.ThrowsAsync<InformacaoException>(() => imagemService.SalvarImagemAsync(form, tipoimagem));
		}
	}
}
