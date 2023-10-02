using OA_Core.Domain.Contracts.Request;
using OA_Core.Domain.Contracts.Response;

namespace OA_Core.Domain.Interfaces.Service
{
	public interface IAvaliacaoService
	{
		Task<Guid> CadastrarAvaliacaoAsync(AvaliacaoRequest avaliacaoRequest);
		Task<Guid> IniciarAvaliacaoAsync(AvaliacaoRequest avaliacaoRequest);
		Task EditarAvaliacaoAsync(Guid id, AvaliacaoRequest avaliacaoRequest);
		Task AtvivarDesativarAvaliacaoAsync(Guid id /*AvaliacaoAtivaDesativaRequest avaliacaoRequest*/);
		Task EncerrarAvaliacaoAsync(Guid id /*AvaliacaoAtivaDesativaRequest avaliacaoRequest*/);
		Task DeletarAvaliacaoAsync(Guid id);
		Task<AvaliacaoResponse> ObterAvaliacaoPorIdAsync(Guid id);
		Task<IEnumerable<AvaliacaoResponse>> ObterTodasAvaliacoesAsync(int page, int rows);
	}
}
