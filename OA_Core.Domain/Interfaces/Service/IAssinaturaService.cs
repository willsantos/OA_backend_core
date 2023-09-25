using OA_Core.Domain.Contracts.Request;

namespace OA_Core.Domain.Interfaces.Service
{
	public interface IAssinaturaService
	{
		Task<Guid> PostAssinaturaAsync(AssinaturaRequest assinatura);
		Task<Guid> PutCancelarAssinaturaAsync(AssinaturaCancelamentoRequest assinatura);
	}
}
