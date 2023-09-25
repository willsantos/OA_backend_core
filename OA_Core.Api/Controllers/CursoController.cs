using Microsoft.AspNetCore.Mvc;
using OA_Core.Domain.Contracts.Request;
using OA_Core.Domain.Contracts.Response;
using OA_Core.Domain.Entities;
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
		private readonly ICursoService _cursoService;
		private readonly ICursoProfessorService _cursoProfessorservice;


		public CursoController(ICursoService cursoService, ICursoProfessorService cursoProfessorService)
		{
			_cursoService = cursoService;
			_cursoProfessorservice = cursoProfessorService;
		}

		[HttpPost("cadastro", Name = "PostCursoAsync")]
		[ProducesResponseType(201)]
		public async Task<ActionResult> PostCursoAsync([FromBody] CursoRequest request)
		{
			var id = await _cursoService.PostCursoAsync(request);
			return Created(nameof(PostCursoAsync), id);
		}

		[HttpGet]
		[ProducesResponseType(200)]
		public async Task<ActionResult<PaginationResponse<CursoResponse>>> GetAllCursoAsync([FromQuery] int page = 0, [FromQuery] int rows = 25)
		{
			var listResponse = await _cursoService.GetAllCursosAsync(page, rows);
			var paginationResponse = new PaginationResponse<CursoResponse>(page, rows, listResponse);

			return Ok(paginationResponse);
		}

		[HttpGet("{id}", Name = "GetCursoByIdAsync")]
		[ProducesResponseType(200)]
		public async Task<ActionResult<CursoResponse>> GetCursoByIdAsync([FromRoute] Guid id)
		{
			var response = await _cursoService.GetCursoByIdAsync(id);

			return Ok(response);
		}

		[HttpPut("{id}")]
		[ProducesResponseType(204)]
		public async Task<ActionResult> PutCursoAsync([FromRoute] Guid id, [FromBody] CursoRequestPut request)
		{
			await _cursoService.PutCursoAsync(id, request);

			return NoContent();
		}

		[HttpDelete("{id}")]
		[ProducesResponseType(204)]
		public async Task<ActionResult> DeleteCursoAsync([FromRoute] Guid id)
		{
			await _cursoService.DeleteCursoAsync(id);

			return NoContent();
		}

		[HttpGet("{cursoId}/professores", Name = "GetProfessoresByCursoIdAsync")]
		[ProducesResponseType(200)]
		public async Task<ActionResult<IEnumerable<ProfessorResponseComResponsavel>>> GetProfessoresByCursoIdAsync([FromRoute] Guid cursoId)
		{
			var professores = await _cursoProfessorservice.GetProfessorDeCursoByIdAsync(cursoId);
			return Ok(professores);
		}

		[HttpPost("{cursoId}/professores", Name = "PostProfessorToCursoAsync")]
		[ProducesResponseType(201)]
		public async Task<ActionResult> PostProfessorToCursoAsync([FromBody] CursoProfessorRequest request)
		{
			var cursoProfessorId = await _cursoProfessorservice.PostCursoProfessorAsync(request);
			return Created(nameof(PostProfessorToCursoAsync), cursoProfessorId);
		}

		[HttpDelete("{cursoProfessorId}/professores/{professorId}", Name = "DeleteProfessorFromCursoAsync")]
		[ProducesResponseType(204)]
		public async Task<ActionResult> DeleteProfessorFromCursoAsync([FromRoute] Guid cursoProfessorId)
		{

			await _cursoProfessorservice.DeleteCursoProfessorAsync(cursoProfessorId);
			return NoContent();
		}
	}
}
