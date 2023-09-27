using OA_Core.Domain.Utils;
using OA_Core.Domain.Validations;

namespace OA_Core.Domain.Entities
{
	public class Assinatura : Entidade
	{
		public Assinatura(Guid usuarioId, AssinaturaTipoEnum tipo, AssinaturaStatusEnum status)
		{
			Id = Guid.NewGuid();
			DataCriacao = DateTime.Now;					
			UsuarioId = usuarioId;
			Tipo = tipo;
			Status = status;					
			Validate(this, new AssinaturaValidator());
		}

		public Assinatura(Guid id, string motivoCancelamento)
		{
			Id = id;			
			MotivoCancelamento = motivoCancelamento;
		}
		public Guid Id { get; set; }
		public Guid UsuarioId { get; set; }
		public virtual Usuario Usuario { get; set; }
		public AssinaturaTipoEnum Tipo { get; set; }
		public AssinaturaStatusEnum Status { get; set; }
		public DateTime DataAtivacao { get; set; }
		public DateTime DataVencimento { get; set; }
		public DateTime? DataCancelamento { get; set; }
		public string? MotivoCancelamento { get; set; }

	}
}
