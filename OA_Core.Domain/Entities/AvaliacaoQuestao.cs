using OA_Core.Domain.Enums;

namespace OA_Core.Domain.Entities
{
	public class AvaliacaoQuestao : Entidade
	{
		public string Enunciado { get; set; }
		public string AlternativaA { get; set; }
		public string AlternativaB { get; set; }
		public string? AlternativaC { get; set; }
		public string? AlternativaD { get; set; }
		public string? AlternativaE { get; set; }
		public Guid AvaliacaoId { get; set; }
		public virtual Avaliacao Avaliacao { get; set; }
		public QuestaoAlternativaEnum RespostaCorreta { get; set; }
		public bool Cancelada { get; set; }
		public string? CanceladaMotivo { get; set; }
		public DateTime? CanceladaEm { get; set; }
	}
}
