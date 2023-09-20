using Microsoft.AspNetCore.Mvc;
using OA_Core.Domain.Enums;
using OA_Core.Domain.Interfaces.Service;

namespace OA_Core.Api.Controllers
{
	[ApiController]
	[Route("api/imagems")]
	public class ImagemController : ControllerBase
	{
		private readonly IImagemService _imagemService;

		public ImagemController(IImagemService imagemService)
		{
			_imagemService = imagemService;
		}

		[HttpPost("upload")]
		public async Task<IActionResult> UploadImagem([FromForm] IFormFile file, TipoImagem tipoImagem)
		{
			var imageUrl = await _imagemService.SaveImageAsync(file, tipoImagem);

			return Ok(imageUrl);
		}
	}

}
