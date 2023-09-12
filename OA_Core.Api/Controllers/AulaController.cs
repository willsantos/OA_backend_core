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

        [HttpPost("cadastro", Name = "PostAulaAsync")]
        [ProducesResponseType(201)]
        public async Task<ActionResult> PostAulaAsync([FromBody] AulaRequest request)
        {
            var id = await _service.PostAulaAsync(request);
            return Created(nameof(PostAulaAsync), id);
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<PaginationResponse<AulaResponse>>> GetAllAulaAsync([FromQuery] int page = 0, [FromQuery] int rows = 25)
        {
            var listResponse = await _service.GetAllAulasAsync(page, rows);
            var paginationResponse = new PaginationResponse<AulaResponse>(page, rows, listResponse);

            return Ok(paginationResponse);
        }

        [HttpGet("{id}", Name = "GetAulaByIdAsync")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<AulaResponse>> GetAulaByIdAsync([FromRoute] Guid id)
        {
            var response = await _service.GetAulaByIdAsync(id);

            return Ok(response);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        public async Task<ActionResult> PutAulaAsync([FromRoute] Guid id, [FromBody] AulaRequestPut request)
        {
            await _service.PutAulaAsync(id, request);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        public async Task<ActionResult> DeleteAulaAsync([FromRoute] Guid id)
        {
            await _service.DeleteAulaAsync(id);

            return NoContent();
        }
    }
}
