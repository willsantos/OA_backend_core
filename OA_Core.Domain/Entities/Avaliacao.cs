using OA_Core.Domain.Enums;
using OA_Core.Domain.Validations;

namespace OA_Core.Domain.Entities
{
    public class Avaliacao : Entidade
    {
		public Avaliacao(string nome, string? descricao, 
						AvaliacaoTipoEnum tipo, double? notaMaxima, 
						double? notaMinima, DateTime? tempo, int? totalQuestoes, 
						bool ativa, DateTime? dataEntrega, Guid aulaId)
		{
			Id = Guid.NewGuid();
			Nome = nome;
			Descricao = descricao;
			Tipo = tipo;
			NotaMaxima = notaMaxima;
			NotaMinima = notaMinima;
			Tempo = tempo;
			TotalQuestoes = totalQuestoes;
			Ativa = ativa;
			DataEntrega = dataEntrega;
			AulaId = aulaId;
			Validate(this, new AvaliacaoValidator());
		}

		public string Nome { get; set; }
        public string? Descricao { get; set; }
        public AvaliacaoTipoEnum Tipo { get; set; }
        public double? NotaMaxima { get; set; }
        public double? NotaMinima { get; set; }
		public DateTime? Tempo { get; set; }
		public int? TotalQuestoes { get; set; } 
		public bool Ativa { get; set; }
        public DateTime? DataEntrega { get; set; }
        public Guid AulaId { get; set; }
		public virtual Aula Aula { get; set; }
    }
}
