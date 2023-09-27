using OA_Core.Domain.Contracts.Request;

namespace OA_Core.Domain.Interfaces.Service
{
	public interface IAssinaturaService
	{
		Task<Guid> AdicionarAssinaturaAsync(AssinaturaRequest assinatura);
		Task<Guid> CancelarAssinaturaAsync(Guid Id, AssinaturaCancelamentoRequest assinatura);
	}
}
