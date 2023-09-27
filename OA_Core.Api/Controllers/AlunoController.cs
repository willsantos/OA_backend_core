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

        [HttpPost("cadastro", Name = "CadastrarAluno")]
        [ProducesResponseType(201)]
        public async Task<ActionResult<Guid>> CadastrarAluno([FromBody] AlunoRequest request)
        {
            var id = await _service.PostAlunoAsync(request);

            return CreatedAtRoute("ObterAlunoPorId", new { id }, id);
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<PaginationResponse<AlunoResponse>>> ObterTodosAlunos([FromQuery] int page = 0, [FromQuery] int rows = 25)
        {
            var listResponse = await _service.GetAllAlunosAsync(page, rows);
            var paginationResponse = new PaginationResponse<AlunoResponse>(page, rows, listResponse);

            return Ok(paginationResponse);
        }

        [HttpGet("{id}", Name = "ObterAlunoPorId")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<AlunoResponse>> ObterAlunoPorId([FromRoute] Guid id)
        {
            var response = await _service.GetAlunoByIdAsync(id);

            return Ok(response);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        public async Task<ActionResult> EditarAluno([FromRoute] Guid id, [FromBody] AlunoRequestPut request)
        {
            await _service.PutAlunoAsync(id, request);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        public async Task<ActionResult> DeletarAluno([FromRoute] Guid id)
        {
            await _service.DeleteAlunoAsync(id);

            return NoContent();
        }
    }
}
