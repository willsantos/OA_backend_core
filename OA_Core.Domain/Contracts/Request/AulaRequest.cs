using OA_Core.Domain.Enums;

namespace OA_Core.Domain.Contracts.Request
{
    public class AulaRequest
    {
        public string Titulo { get; set; }
        public int Duracao { get; set; }
        public int Ordem { get; set; }
        public TipoAula Tipo { get; set; }
        public Guid CursoId { get; set; }

		public string? Url { get; set; }
		public string? Conteudo{ get; set; }
		public DateTime? HorarioInicio{ get; set; }
		public DateTime? HorarioFim { get; set; }
	}
}
