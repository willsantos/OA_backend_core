using OA_Core.Domain.Contracts.Request;
using OA_Core.Domain.Contracts.Response;
using OA_Core.Domain.Entities;

namespace OA_Core.Domain.Interfaces.Service
{
	public interface IAvaliacaoService
	{
		Task<Guid> CadastrarAvaliacaoAsync(AvaliacaoRequest avaliacaoRequest);
		Task IniciarAvaliacaoAsync(AvaliacaoUsuarioRequest avaliacaoUsuarioRequest);
		Task<AvaliacaoResponse> EditarAvaliacaoAsync(Guid id, AvaliacaoRequest avaliacaoRequest);
		Task AtivivarDesativarAvaliacaoAsync(Guid id, bool ativa);
		Task EncerrarAvaliacaoAsync(AvaliacaoUsuarioRequest avaliacaoUsuarioRequest);
		Task DeletarAvaliacaoAsync(Guid id);
		Task<AvaliacaoResponse> ObterAvaliacaoPorIdAsync(Guid id);
		Task<IEnumerable<AvaliacaoResponse>> ObterTodasAvaliacoesAsync(int page, int rows);
	}
}
