using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using OA_Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_Core.Tests.Service
{
	public class ImagemServiceTests
	{
		[Fact]
		public async Task SaveImageAsync_ValidFile_ReturnsFilePath()
		{
			// Arrange
			var hostingEnvironment = Substitute.For<IHostingEnvironment>();
			hostingEnvironment.ContentRootPath.Returns("mocked_content_root_path");

			var formFile = Substitute.For<IFormFile>();
			formFile.Length.Returns(100); // Set a valid file length
			formFile.FileName.Returns("test_image");

			var imagemService = new ImagemService(hostingEnvironment);

			// Act
			var result = await imagemService.SaveImageAsync(formFile);

			// Assert
			result.Should().NotBeNullOrEmpty();
			result.Should().StartWith("images\\test_image");
		}

		[Fact]
		public async Task SaveImageAsync_InvalidFile_ThrowsException()
		{
			// Arrange
			var hostingEnvironment = Substitute.For<IHostingEnvironment>();
			hostingEnvironment.ContentRootPath.Returns("your_mocked_content_root_path");

			var formFile = Substitute.For<IFormFile>();
			formFile.Length.Returns(0);
			formFile.FileName.Returns("invalid_image.jpg");

			var imagemService = new ImagemService(hostingEnvironment);

			// Act and Assert
			Func<Task> action = async () => await imagemService.SaveImageAsync(formFile);
			await action.Should().ThrowAsync<ArgumentException>();
		}
	}
}
