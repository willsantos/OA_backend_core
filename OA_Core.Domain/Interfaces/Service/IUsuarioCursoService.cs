using OA_Core.Domain.Contracts.Request;
using OA_Core.Domain.Contracts.Response;
using OA_Core.Domain.Entities;

namespace OA_Core.Domain.Interfaces.Service
{
	public interface IUsuarioCursoService
	{
		Task<Guid> PostUsuarioCursoAsync(UsuarioCursoRequest cursoRequest, Guid cursoId);
		Task<List<CursoParaUsuarioResponse>> GetCursoDeUsuarioByIdAsync(Guid cursoId);
	}
}
