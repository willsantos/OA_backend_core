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
    public class ProfessorController : ControllerBase
    {
        private readonly IProfessorService _service;

        public ProfessorController(IProfessorService service)
        {
            _service = service;
        }

        [HttpPost("cadastro", Name = "CadastrarProfessor")]
        [ProducesResponseType(201)]
        public async Task<ActionResult> CadastrarProfessor([FromBody] ProfessorRequest request)
        {
            var id = await _service.PostProfessorAsync(request);
			return Created(nameof(CadastrarProfessor), id);
		}

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<PaginationResponse<ProfessorResponse>>> ObterTodosProfessores([FromQuery] int page = 0, [FromQuery] int rows = 25)
        {
            var listResponse = await _service.GetAllProfessoresAsync(page, rows);
            var paginationResponse = new PaginationResponse<ProfessorResponse>(page, rows, listResponse);

            return Ok(paginationResponse);
        }

        [HttpGet("{id}", Name = "ObterProfessorPorId")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<ProfessorResponse>> ObterProfessorPorId([FromRoute] Guid id)
        {
            var response = await _service.GetProfessorByIdAsync(id);

            return Ok(response);
        }   

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        public async Task<ActionResult> EditarProfessor([FromRoute] Guid id, [FromBody] ProfessorRequestPut request)
        {
            await _service.PutProfessorAsync(id, request);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        public async Task<ActionResult> DeletarProfessor([FromRoute] Guid id)
        {
            await _service.DeleteProfessorAsync(id);

            return NoContent();
        }
    }
}
