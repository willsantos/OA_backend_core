using OA_Core.Domain.Enums;
using System.Text.Json.Serialization;

namespace OA_Core.Domain.Contracts.Response
{
	public class AulaResponse : BaseResponse
	{
		public string Titulo { get; set; }
		public int Duracao { get; set; }
		public int Ordem { get; set; }
		public TipoAula Tipo { get; set; }
		public Guid CursoId { get; set; }

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public string? Url { get; set; }
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public string? Conteudo { get; set; }
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public DateTime? HorarioInicio { get; set; }
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public DateTime? HorarioFim { get; set; }
	}
}
