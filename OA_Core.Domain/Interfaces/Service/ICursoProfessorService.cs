using OA_Core.Domain.Contracts.Request;
using OA_Core.Domain.Contracts.Response;
using OA_Core.Domain.Entities;

namespace OA_Core.Domain.Interfaces.Service
{
	public interface ICursoProfessorService
	{
		Task<Guid> PostCursoProfessorAsync(CursoProfessorRequest curso);
		Task PutCursoProfessorAsync(Guid id, CursoProfessorRequest curso);
		Task DeleteCursoProfessorAsync(Guid id);
		Task<CursoProfessor> GetCursoProfessorByIdAsync(Guid id);
		Task<IEnumerable<CursoProfessor>> GetAllCursoProfessorsAsync(int page, int rows);
		Task<List<ProfessorResponseComResponsavel>> GetProfessorDeCursoByIdAsync(Guid cursoId);
	}
}
