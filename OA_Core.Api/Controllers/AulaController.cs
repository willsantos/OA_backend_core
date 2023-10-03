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
    public class AulaController : ControllerBase
    {
        private readonly IAulaService _service;

        public AulaController(IAulaService service)
        {
            _service = service;
        }

        [HttpPost("cadastro", Name = "CadastrarAula")]
        [ProducesResponseType(201)]
        public async Task<ActionResult> CadastrarAula([FromBody] AulaRequest request)
        {
            var id = await _service.CadastrarAulaAsync(request);
            return Created(nameof(CadastrarAula), id);
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<PaginationResponse<AulaResponse>>> ObterTodasAulas([FromQuery] int page = 0, [FromQuery] int rows = 25)
        {
            var listResponse = await _service.ObterTodasAulasAsync(page, rows);
            var paginationResponse = new PaginationResponse<AulaResponse>(page, rows, listResponse);

            return Ok(paginationResponse);
        }

        [HttpGet("{id}", Name = "ObterAulaPorId")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<AulaResponse>> ObterAulaPorId([FromRoute] Guid id)
        {
            var response = await _service.ObterAulaPorIdAsync(id);

            return Ok(response);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        public async Task<ActionResult> EditarAula([FromRoute] Guid id, [FromBody] AulaRequestPut request)
        {
            await _service.EditarAulaAsync(id, request);

            return NoContent();
        }

		[HttpPatch("{id}/ordens")]
		[ProducesResponseType(204)]
		public async Task<ActionResult> EditarOrdemAula([FromRoute] Guid id, [FromBody] OrdemRequest ordem)
		{
			await _service.EditarOrdemAulaAsync(id, ordem);

			return NoContent();
		}

		[HttpDelete("{id}")]
        [ProducesResponseType(204)]
        public async Task<ActionResult> DeletarAula([FromRoute] Guid id)
        {
            await _service.DeletarAulaAsync(id);

            return NoContent();
        }
    }
}
