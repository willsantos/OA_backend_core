using OA_Core.Domain.Contracts.Response;
using OA_Core.Domain.Entities;

namespace OA_Core.Domain.Interfaces.Service
{
	public interface ICursoProfessorService
	{
		Task<Guid> PostCursoProfessorAsync(CursoProfessor curso);
		Task PutCursoProfessorAsync(Guid id, CursoProfessor curso);
		Task DeleteCursoProfessorAsync(Guid id);
		Task<CursoProfessor> GetCursoProfessorByIdAsync(Guid id);
		Task<IEnumerable<CursoProfessor>> GetAllCursoProfessorsAsync(int page, int rows);
	}
}
