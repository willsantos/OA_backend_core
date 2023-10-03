using OA_Core.Domain.Contracts.Request;
using OA_Core.Domain.Contracts.Response;

namespace OA_Core.Domain.Interfaces.Service
{
    public interface IAulaService
    {
        Task<Guid> CadastrarAulaAsync(AulaRequest aulaRequest);
        Task EditarAulaAsync(Guid id, AulaRequestPut aulaRequest);
        Task DeletarAulaAsync(Guid id);
        Task<AulaResponse> ObterAulaPorIdAsync(Guid id);
        Task<IEnumerable<AulaResponse>> ObterTodasAulasAsync(int page, int rows);
		Task EditarOrdemAulaAsync(Guid id, OrdemRequest ordem);

	}
}
