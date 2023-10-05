using OA_Core.Domain.Enums;
using System.Text.Json.Serialization;

namespace OA_Core.Domain.Contracts.Request
{
    public class AulaRequestPut
    {
        public string Titulo { get; set; }
        public int Duracao { get; set; }
        public int Ordem { get; set; }
        public TipoAula Tipo { get; set; }

		public string? Url { get; set; }
		public string? Conteudo { get; set; }
		public DateTime? HorarioInicio { get; set; }
		public DateTime? HorarioFim { get; set; }
	}
}
