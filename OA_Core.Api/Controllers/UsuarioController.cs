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
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _service;

        public UsuarioController(IUsuarioService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<PaginationResponse<UsuarioResponse>>> GetAllUsuariosAsync([FromQuery] int page = 0, [FromQuery]int rows = 25)
        {
            var listResponse = await _service.GetAllUsuariosAsync(page, rows);
            var paginationResponse = new PaginationResponse<UsuarioResponse>(page, rows, listResponse);

            return Ok(paginationResponse);
        }

        [HttpGet("{id}", Name = "GetUsuarioByIdAsync")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<UsuarioResponse>> GetUsuarioByIdAsync([FromRoute] Guid id)
        {
            var response = await _service.GetUsuarioByIdAsync(id);

            return Ok(response);
        }

        [HttpPost("cadastro", Name = "PostUsuarioAsync")]
        [ProducesResponseType(201)]
        public async Task<ActionResult<Guid>> PostUsuarioAsync([FromBody] UsuarioRequest request)
        {         
            var id = await _service.PostUsuarioAsync(request);

            return CreatedAtRoute("GetUsuarioByIdAsync", new { id }, id);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        public async Task<ActionResult> PutUsuarioAsync([FromRoute] Guid id, [FromBody] UsuarioRequest request)
        {
            await _service.PutUsuarioAsync(id, request);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        public async Task<ActionResult> DeleteUsuarioAsync([FromRoute] Guid id)
        {
            await _service.DeleteUsuarioAsync(id);

            return NoContent();
        }
    }
}
