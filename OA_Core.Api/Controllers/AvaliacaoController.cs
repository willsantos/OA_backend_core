using Microsoft.AspNetCore.Mvc;
using OA_Core.Domain.Contracts.Request;
using OA_Core.Domain.Contracts.Response;
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

		[HttpPost("Ativar", Name = "CadastrarAvaliacaoUsuario")]
		[ProducesResponseType(204)]
		public async Task<ActionResult> IniciarAvaliacaoUsuario([FromBody] AvaliacaoUsuarioRequest request)
		{
			await _service.IniciarAvaliacaoAsync(request);

			return NoContent();
		}

		[HttpPatch("Encerrar", Name = "EncerrarAvaliacaoUsuario")]
		[ProducesResponseType(204)]
		public async Task<ActionResult> EncerrarAvaliacaoUsuario([FromBody] AvaliacaoUsuarioRequest request)
		{
			await _service.EncerrarAvaliacaoAsync(request);

			return NoContent();
		}

		[HttpPatch("{id}/AtivarDesativar", Name = "AtivarDesativarAvaliacao")]
		[ProducesResponseType(204)]
		public async Task<ActionResult> AtivarDesativarAvaliacao([FromBody] bool avaliar, [FromRoute]Guid id)
		{
			await _service.AtivivarDesativarAvaliacaoAsync(id, avaliar);

			return NoContent();
		}

		[HttpPut("{id}", Name = "EditarAvaliacao")]
		[ProducesResponseType(204)]
		public async Task<ActionResult<AvaliacaoResponse>> EditarAvaliacao([FromBody] AvaliacaoRequest request, [FromRoute] Guid id)
		{
			var entity = await _service.EditarAvaliacaoAsync(id, request);

			return Ok(entity);
		}

		[HttpDelete("{id}", Name = "DeletarAvaliacao")]
		[ProducesResponseType(204)]
		public async Task<ActionResult> DeletarAvaliacao([FromRoute] Guid id)
		{
			await _service.DeletarAvaliacaoAsync(id);

			return NoContent();
		}

		//Excluir
		//Buscar por Id
		//Buscar todos	
	}
}
