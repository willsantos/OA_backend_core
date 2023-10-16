using OA_Core.Domain.Enums;

namespace OA_Core.Domain.Entities
{
	public class AulaDownload : Aula
	{
		public AulaDownload(string titulo, TipoAula tipo, int duracao, int ordem, Guid cursoId, string url) : base(titulo, tipo, duracao, ordem, cursoId)
		{
			Url = url;
		}

		public AulaDownload(string titulo, TipoAula tipo, int duracao, int ordem, string url) : base(titulo, tipo, duracao, ordem)
		{
			Url = url;
		}

		public string Url { get; set; }
	}
}
