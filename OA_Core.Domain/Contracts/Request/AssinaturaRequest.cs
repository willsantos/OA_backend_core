using OA_Core.Domain.Utils;

namespace OA_Core.Domain.Contracts.Request
{
	public class AssinaturaRequest
	{
		public Guid UsuarioId { get; set; }	
		public AssinaturaTipoEnum Tipo { get; set; }
		public AssinaturaStatusEnum Status { get; set; }	
	}
}
