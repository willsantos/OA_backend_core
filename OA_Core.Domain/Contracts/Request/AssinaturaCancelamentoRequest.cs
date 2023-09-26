
using OA_Core.Domain.Utils;

namespace OA_Core.Domain.Contracts.Request
{
	public class AssinaturaCancelamentoRequest
	{
		public Guid Id { get; set; }
		public string MotivoCancelamento { get; set; }		

	}
}
