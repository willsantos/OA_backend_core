using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OA_Core.Domain.Contracts.Request;
using OA_Core.Domain.Contracts.Response;
using OA_Core.Domain.Entities;
using OA_Core.Domain.Interfaces.Service;

namespace OA_Core.Api.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _service;
        private readonly IMapper _mapper;

        public UsuarioController(IUsuarioService service, IMapper mapper)
        {
            _mapper = mapper;
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<PaginationResponse<UsuarioResponse>>> GetAllUsuariosAsync([FromQuery] int page = 0, [FromQuery]int rows = 25)
        {
            var listEntity = await _service.GetAllUsuariosAsync(page, rows);
            var listReponse = _mapper.Map<IEnumerable<UsuarioResponse>>(listEntity);
            var paginationResponse = new PaginationResponse<UsuarioResponse>(page, rows, listReponse);

            return Ok(paginationResponse);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioResponse>> GetUsuarioByIdAsync([FromRoute] Guid id)
        {
            var entity = await _service.GetUsuarioByIdAsync(id);

            var response = _mapper.Map<UsuarioResponse>(entity);

            return Ok(response);
        }

        [HttpPost("cadastro")]
        public async Task<ActionResult> PostUsuarioAsync([FromBody] UsuarioRequest request)
        {
            var entity = _mapper.Map<Usuario>(request);

            var id = await _service.PostUsuarioAsync(entity);

            return Created(nameof(PostUsuarioAsync), new { id });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutUsuarioAsync([FromRoute] Guid id, [FromBody] UsuarioRequest request)
        {
            var entity = _mapper.Map<Usuario>(request);

            await _service.PutUsuarioAsync(id, entity);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUsuarioAsync([FromRoute] Guid id)
        {
            await _service.DeleteUsuarioAsync(id);

            return NoContent();
        }
    }
}
