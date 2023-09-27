
using OA_Core.Domain.Entities;
using OA_Core.Domain.Utils;

namespace OA_Core.Domain.Contracts.Response
{
	public class AssinaturaResponse
	{
		public Guid Id { get; set; }
		public Guid UsuarioId { get; set; }
		public AssinaturaTipoEnum Tipo { get; set; }
		public AssinaturaStatusEnum Status { get; set; }
		public DateTime DataAtivacao { get; set; }
		public DateTime DataVencimento { get; set; }
		public DateTime DataCancelamento { get; set; }
		public string MotivoCancelamento { get; set; }
	}
}
