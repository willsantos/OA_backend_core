using Microsoft.AspNetCore.Mvc;
using OA_Core.Domain.Contracts.Request;
using OA_Core.Domain.Contracts.Response;
using OA_Core.Domain.Interfaces.Service;

namespace OA_Core.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	[ProducesResponseType(typeof(InformacaoResponse), 400)]
	[ProducesResponseType(typeof(InformacaoResponse), 401)]
	[ProducesResponseType(typeof(InformacaoResponse), 403)]
	[ProducesResponseType(typeof(InformacaoResponse), 404)]
	[ProducesResponseType(typeof(InformacaoResponse), 500)]
	public class AssinaturaController : ControllerBase
	{
		private readonly IAssinaturaService _assinaturaService;

		public AssinaturaController(IAssinaturaService assinaturaService)
		{
			_assinaturaService = assinaturaService;
		}
		[HttpPost("cadastro", Name = "AdicionarAssinatura")]
		[ProducesResponseType(201)]
		public async Task<ActionResult> AdicionarAssinatura([FromBody] AssinaturaRequest request)
		{
			var id = await _assinaturaService.AdicionarAssinaturaAsync(request);

			return Created(nameof(AdicionarAssinatura), id);
		}

		[HttpPut("{id}")]
		[ProducesResponseType(204)]
		public async Task<ActionResult> CancelarAssinatura([FromRoute] Guid id, [FromBody] AssinaturaCancelamentoRequest request)
		{
			await _assinaturaService.CancelarAssinaturaAsync(id, request);

			return NoContent();
		}
	}
}
