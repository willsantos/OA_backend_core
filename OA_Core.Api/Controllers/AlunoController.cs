using Microsoft.AspNetCore.Mvc;
using OA_Core.Domain.Contracts.Request;
using OA_Core.Domain.Contracts.Response;
using OA_Core.Domain.Interfaces.Service;

namespace OA_Core.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {
        private readonly IAlunoService _service;

        public AlunoController(IAlunoService service)
        {
            _service = service;
        }

        [HttpPost("cadastro", Name = "PostAlunoAsync")]
        [ProducesResponseType(201)]
        public async Task<ActionResult<Guid>> PostAlunoAsync([FromBody] AlunoRequest request)
        {
            var id = await _service.PostAlunoAsync(request);

            return CreatedAtRoute("GetAlunoByIdAsync", new { id }, id);
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<PaginationResponse<AlunoResponse>>> GetAllAlunosAsync([FromQuery] int page = 0, [FromQuery] int rows = 25)
        {
            var listResponse = await _service.GetAllAlunosAsync(page, rows);
            var paginationResponse = new PaginationResponse<AlunoResponse>(page, rows, listResponse);

            return Ok(paginationResponse);
        }

        [HttpGet("{id}", Name = "GetAlunoByIdAsync")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<AlunoResponse>> GetAlunoByIdAsync([FromRoute] Guid id)
        {
            var response = await _service.GetAlunoByIdAsync(id);

            return Ok(response);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        public async Task<ActionResult> PutAlunoAsync([FromRoute] Guid id, [FromBody] AlunoRequestPut request)
        {
            await _service.PutAlunoAsync(id, request);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        public async Task<ActionResult> DeleteAlunoAsync([FromRoute] Guid id)
        {
            await _service.DeleteAlunoAsync(id);

            return NoContent();
        }
    }
}
