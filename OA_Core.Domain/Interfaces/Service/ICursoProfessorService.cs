using OA_Core.Domain.Contracts.Request;
using OA_Core.Domain.Contracts.Response;
using OA_Core.Domain.Entities;

namespace OA_Core.Domain.Interfaces.Service
{
	public interface ICursoProfessorService
	{
		Task<Guid> CadastrarCursoProfessorAsync(CursoProfessorRequest cursoRequest, Guid cursoId);
		Task EditarCursoProfessorAsync(Guid Cursoid, CursoProfessorRequest cursoRequest);
		Task DeletarCursoProfessorAsync(Guid cursoId, Guid professorId);
		Task<CursoProfessor> ObterCursoProfessorPorIdAsync(Guid id);
		Task<IEnumerable<CursoProfessor>> ObterTodosCursoProfessoresAsync(int page, int rows);
		Task<List<ProfessorResponseComResponsavel>> ObterProfessoresDeCursoPorIdAsync(Guid cursoId);
	}
}
