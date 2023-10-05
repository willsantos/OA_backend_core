using OA_Core.Domain.Enums;

namespace OA_Core.Domain.Entities
{
	public class AulaTexto : Aula
	{
		public AulaTexto(string titulo, TipoAula tipo, int duracao, int ordem, Guid cursoId, string conteudo) : base(titulo, tipo, duracao, ordem, cursoId)
		{
			Conteudo = conteudo;
		}

		public AulaTexto(string titulo, TipoAula tipo, int duracao, int ordem, string conteudo) : base(titulo, tipo, duracao, ordem)
		{
			Conteudo = conteudo;
		}

		public string Conteudo { get; set; }
	}
}
