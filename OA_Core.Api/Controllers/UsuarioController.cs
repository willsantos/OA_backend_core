using Microsoft.AspNetCore.Mvc;
using OA_Core.Domain.Contracts.Request;
using OA_Core.Domain.Contracts.Response;
using OA_Core.Domain.Interfaces.Service;
using OA_Core.Service;

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
		private readonly IUsuarioCursoService _usuarioCursoService;

        public UsuarioController(IUsuarioService service, IUsuarioCursoService usuarioCursoService)
        {
            _service = service;
			_usuarioCursoService = usuarioCursoService;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<PaginationResponse<UsuarioResponse>>> ObterTodosUsuarios([FromQuery] int page = 0, [FromQuery]int rows = 25)
        {
            var listResponse = await _service.ObterTodosUsuariosAsync(page, rows);
            var paginationResponse = new PaginationResponse<UsuarioResponse>(page, rows, listResponse);

            return Ok(paginationResponse);
        }

        [HttpGet("{id}", Name = "ObterUsuarioPorId")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<UsuarioResponse>> ObterUsuarioPorId([FromRoute] Guid id)
        {
            var response = await _service.ObterUsuarioPorIdAsync(id);

            return Ok(response);
        }

        [HttpPost("cadastro", Name = "CadastrarUsuario")]
        [ProducesResponseType(201)]
        public async Task<ActionResult<Guid>> CadastrarUsuario([FromBody] UsuarioRequest request)
        {         
            var id = await _service.CadastrarUsuarioAsync(request);

            return CreatedAtRoute("ObterUsuarioPorId", new { id }, id);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        public async Task<ActionResult> EditarUsuario([FromRoute] Guid id, [FromBody] UsuarioRequest request)
        {
            await _service.EditarUsuarioAsync(id, request);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        public async Task<ActionResult> DeletarUsuario([FromRoute] Guid id)
        {
            await _service.DeletarUsuarioAsync(id);

            return NoContent();
        }

		[HttpGet("usuario-curso/{usuarioId}", Name = "ObterCursosDeUsuarioPorId")]
		[ProducesResponseType(200)]
		public async Task<ActionResult<IEnumerable<CursoParaUsuarioResponse>>> ObterCursosDeUsuarioPorId([FromRoute] Guid usuarioId)
		{
			var cursos = await _usuarioCursoService.ObterCursosDeUsuarioPorIdAsync(usuarioId);
			return Ok(cursos);
		}

		[HttpPost("usuario-curso", Name = "CadastrarCursoAUsuario")]
		[ProducesResponseType(201)]
		public async Task<ActionResult> CadastrarCursoAUsuario([FromBody] UsuarioCursoRequest request)
		{
			var usuarioCursoId = await _usuarioCursoService.CadastrarUsuarioACursoAsync(request);
			return Created(nameof(CadastrarCursoAUsuario), usuarioCursoId);
		}
	}
}
