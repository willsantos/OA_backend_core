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

		[HttpPost("enviar")]
		public async Task<IActionResult> EnviarImagem([FromForm] IFormFile file, TipoImagem tipoImagem)
		{
			var imageUrl = await _imagemService.SalvarImagemAsync(file, tipoImagem);

			return Ok(imageUrl);
		}
	}

}
