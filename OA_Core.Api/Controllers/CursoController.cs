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
    public class CursoController : ControllerBase
    {
        private readonly ICursoService _service;

        public CursoController(ICursoService service)
        {
            _service = service;
        }

        [HttpPost("cadastro", Name = "PostCursoAsync")]
        [ProducesResponseType(201)]
        public async Task<ActionResult> PostCursoAsync([FromBody] CursoRequest request)
        {
            var id = await _service.PostCursoAsync(request);
            return Created(nameof(PostCursoAsync), id);
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<PaginationResponse<CursoResponse>>> GetAllCursoAsync([FromQuery] int page = 0, [FromQuery] int rows = 25)
        {
            var listResponse = await _service.GetAllCursosAsync(page, rows);
            var paginationResponse = new PaginationResponse<CursoResponse>(page, rows, listResponse);

            return Ok(paginationResponse);
        }

        [HttpGet("{id}", Name = "GetCursoByIdAsync")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<CursoResponse>> GetCursoByIdAsync([FromRoute] Guid id)
        {
            var response = await _service.GetCursoByIdAsync(id);

            return Ok(response);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        public async Task<ActionResult> PutCursoAsync([FromRoute] Guid id, [FromBody] CursoRequestPut request)
        {
            await _service.PutCursoAsync(id, request);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        public async Task<ActionResult> DeleteCursoAsync([FromRoute] Guid id)
        {
            await _service.DeleteCursoAsync(id);

            return NoContent();
        }
    }
}
