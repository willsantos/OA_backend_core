using OA_Core.Domain.Contracts.Request;
using OA_Core.Domain.Entities;

namespace OA_Core.Domain.Interfaces.Repository
{
	public interface IAssinaturaRepository
	{
		Task<Guid> CadastrarAssinaturaAsync(Assinatura assinatura);
		Task<Guid> CancelarAssinaturaAsync(Assinatura assinatura);
		Task<Assinatura> BuscarAssinaturaPorUsuarioId(Guid UsuarioId);
	}
}
