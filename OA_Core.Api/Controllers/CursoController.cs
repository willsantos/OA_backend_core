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

		[HttpPost("cadastro", Name = "CadastrarCurso")]
		[ProducesResponseType(201)]
		public async Task<ActionResult> CadastrarCurso([FromBody] CursoRequest request)
		{
			var id = await _cursoService.CadastrarCursoAsync(request);
			return Created(nameof(CadastrarCurso), id);
		}

		[HttpGet]
		[ProducesResponseType(200)]
		public async Task<ActionResult<PaginationResponse<CursoResponse>>> ObterTodosCursos([FromQuery] int page = 0, [FromQuery] int rows = 25)
		{
			var listResponse = await _cursoService.ObterTodosCursosAsync(page, rows);
			var paginationResponse = new PaginationResponse<CursoResponse>(page, rows, listResponse);

			return Ok(paginationResponse);
		}

		[HttpGet("{id}", Name = "ObterCursoPorId")]
		[ProducesResponseType(200)]
		public async Task<ActionResult<CursoResponse>> ObterCursoPorId([FromRoute] Guid id)
		{
			var response = await _cursoService.ObterCursoPorIdAsync(id);

			return Ok(response);
		}

		[HttpPut("{id}")]
		[ProducesResponseType(204)]
		public async Task<ActionResult> EditarCurso([FromRoute] Guid id, [FromBody] CursoRequestPut request)
		{
			await _cursoService.EditarCursoAsync(id, request);

			return NoContent();
		}

		[HttpDelete("{id}")]
		[ProducesResponseType(204)]
		public async Task<ActionResult> DeletarCurso([FromRoute] Guid id)
		{
			await _cursoService.DeletarCursoAsync(id);

			return NoContent();
		}

		[HttpGet("{cursoId}/professores", Name = "ObterProfessoresDeCurso")]
		[ProducesResponseType(200)]
		public async Task<ActionResult<IEnumerable<ProfessorResponseComResponsavel>>> ObterProfessoresDeCurso([FromRoute] Guid cursoId)
		{
			var professores = await _cursoProfessorservice.ObterProfessoresDeCursoPorIdAsync(cursoId);
			return Ok(professores);
		}

		[HttpPost("{cursoId}/professores", Name = "CadastrarProfessorACurso")]
		[ProducesResponseType(201)]
		public async Task<ActionResult> CadastrarProfessorACurso([FromBody] CursoProfessorRequest request, Guid cursoId)
		{
			var cursoProfessorId = await _cursoProfessorservice.CadastrarCursoProfessorAsync(request, cursoId);
			return Created(nameof(CadastrarProfessorACurso), cursoProfessorId);
		}

		[HttpDelete("{cursoId}/professores/{professorId}", Name = "DeletarProfessorDeCurso")]
		[ProducesResponseType(204)]
		public async Task<ActionResult> DeletarProfessorDeCurso([FromRoute] Guid cursoId, Guid professorId)
		{

			await _cursoProfessorservice.DeletarCursoProfessorAsync(cursoId, professorId);
			return NoContent();
		}
	}
}
