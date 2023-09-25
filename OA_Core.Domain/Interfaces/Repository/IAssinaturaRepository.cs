using OA_Core.Domain.Contracts.Request;

namespace OA_Core.Domain.Interfaces.Repository
{
	public interface IAssinaturaRepository
	{
		Task<Guid> CadastrarAssinaturaAsync(AssinaturaRequest assinatura);
		Task<Guid> CancelarAssinaturaAsync(AssinaturaCancelamentoRequest assinatura);
	}
}
