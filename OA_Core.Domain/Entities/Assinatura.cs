using OA_Core.Domain.Utils;
using OA_Core.Domain.Validations;

namespace OA_Core.Domain.Entities
{
	public class Assinatura : Entidade
	{
		public Assinatura(Guid usuarioId, AssinaturaTipoEnum tipo, AssinaturaStatusEnum status, DateTime dataAtivacao, DateTime dataVencimento, DateTime dataCancelamento, string motivoCancelamento)
		{
			Id = Guid.NewGuid();
			DataCriacao = DateTime.Now;		
			DataAlteracao = null;
			UsuarioId = usuarioId;
			Tipo = tipo;
			Status = status;
			DataAtivacao = dataAtivacao;
			DataVencimento = dataVencimento;
			DataCancelamento = dataCancelamento;
			MotivoCancelamento = motivoCancelamento;
			Validate(this, new AssinaturaValidator());
		}

		public Assinatura(Guid usuarioId, Guid id, AssinaturaTipoEnum tipo, AssinaturaStatusEnum status, DateTime dataCriacao, DateTime dataAtivacao, DateTime dataVencimento, DateTime dataCancelamento, string motivoCancelamento)
		{
			Id = id;
			UsuarioId = usuarioId;
			Tipo = tipo;
			Status = status;
			DataAtivacao = dataAtivacao;
			DataVencimento = dataVencimento;
			DataCancelamento = dataCancelamento;
			MotivoCancelamento = motivoCancelamento;
		}

		public Guid UsuarioId { get; set; }
		public virtual Usuario Usuario { get; set; }
		public AssinaturaTipoEnum Tipo { get; set; }
		public AssinaturaStatusEnum Status { get; set; }
		public DateTime DataAtivacao { get; set; }
		public DateTime DataVencimento { get; set; }
		public DateTime DataCancelamento { get; set; }
		public string MotivoCancelamento { get; set; }

	}
}
