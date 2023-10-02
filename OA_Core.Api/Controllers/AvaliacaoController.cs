using Microsoft.AspNetCore.Mvc;
using OA_Core.Domain.Contracts.Request;
using OA_Core.Domain.Interfaces.Service;

namespace OA_Core.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AvaliacaoController : ControllerBase
	{
		private readonly IAvaliacaoService _service;

		public AvaliacaoController(IAvaliacaoService service)
		{
			_service = service;
		}
		[HttpPost("cadastro", Name = "CadastrarAvaliacao")]
		[ProducesResponseType(201)]
		public async Task<ActionResult<Guid>> CadastrarAvaliacao([FromBody] AvaliacaoRequest request)
		{
			var id = await _service.CadastrarAvaliacaoAsync(request);

			return Created(nameof(CadastrarAvaliacao), id);
		}
	}
}
