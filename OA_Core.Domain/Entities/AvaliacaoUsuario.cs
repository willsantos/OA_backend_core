namespace OA_Core.Domain.Entities
{
	public class AvaliacaoUsuario
	{
		public Guid AvaliacaoId{ get; set; }
		public virtual Avaliacao Avaliacao { get; set; }
		public Guid UsuarioId { get; set; }
		public virtual Usuario Usuario { get; set; }
		public double? NotaObtida { get; set; }
		public bool Aprovado { get; set; }
		public DateTime Inicio { get; set; }
		public DateTime? Fim { get; set; }
	}
}
