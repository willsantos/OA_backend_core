using OA_Core.Domain.Enums;
using OA_Core.Domain.Validations;

namespace OA_Core.Domain.Entities
{
    public abstract class Aula : Entidade
    {
        protected Aula(string titulo, TipoAula tipo, int duracao, int ordem, Guid cursoId)
        { 
            Id = Guid.NewGuid();
            Titulo = titulo;
            Duracao = duracao;
            Ordem = ordem;
            Tipo = tipo;
            CursoId = cursoId;
            Validate(this, new AulaValidator());
        }

        protected Aula(string titulo, TipoAula tipo, int duracao, int ordem)
        {
            Titulo = titulo;
            Duracao = duracao;
            Ordem = ordem;
            Tipo = tipo;
            Validate(this, new AulaValidator());
        }

        public string Titulo { get; set; }
        public int Duracao { get; set; }
        public int Ordem { get; set; }
        public TipoAula Tipo { get; set; }
        public Guid CursoId { get; set; }
		public virtual Curso curso { get; set; }

		public virtual ICollection<UsuarioAula> UsuarioAulas { get; set; } = new List<UsuarioAula>();
	}
}
