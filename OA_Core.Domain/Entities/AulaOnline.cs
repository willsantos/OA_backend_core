using OA_Core.Domain.Enums;

namespace OA_Core.Domain.Entities
{
	public class AulaOnline : Aula
	{
		public AulaOnline(string titulo, TipoAula tipo, int duracao, int ordem, Guid cursoId, string url, DateTime horarioInicio, DateTime horarioFim) : base(titulo, tipo, duracao, ordem, cursoId)
		{
			Url = url;
			HorarioInicio = horarioInicio;
			HorarioFim = horarioFim;
		}

		public AulaOnline(string titulo, TipoAula tipo, int duracao, int ordem, string url, DateTime horarioInicio, DateTime horarioFim) : base(titulo, tipo, duracao, ordem)
		{
			Url = url;
			HorarioInicio = horarioInicio;
			HorarioFim = horarioFim;
		}

		public string Url { get; set; }
		public DateTime HorarioInicio { get; set; }
		public DateTime HorarioFim { get; set; }
	}
}
