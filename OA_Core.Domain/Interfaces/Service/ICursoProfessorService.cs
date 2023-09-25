using OA_Core.Domain.Contracts.Request;
using OA_Core.Domain.Contracts.Response;
using OA_Core.Domain.Entities;

namespace OA_Core.Domain.Interfaces.Service
{
	public interface ICursoProfessorService
	{
		Task<Guid> PostCursoProfessorAsync(CursoProfessorRequest cursoRequest, Guid cursoId);
		Task PutCursoProfessorAsync(Guid Cursoid, CursoProfessorRequest cursoRequest);
		Task DeleteCursoProfessorAsync(Guid cursoId, Guid professorId);
		Task<CursoProfessor> GetCursoProfessorByIdAsync(Guid id);
		Task<IEnumerable<CursoProfessor>> GetAllCursoProfessorsAsync(int page, int rows);
		Task<List<ProfessorResponseComResponsavel>> GetProfessorDeCursoByIdAsync(Guid cursoId);
	}
}
