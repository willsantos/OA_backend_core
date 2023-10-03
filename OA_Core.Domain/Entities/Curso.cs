using OA_Core.Domain.Validations;

namespace OA_Core.Domain.Entities
{
    public class Curso : Entidade
    {
        public Curso(string nome, string descricao, string categoria, string preRequisito, double preco, int aulas, Guid professorId)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Descricao = descricao;
            Categoria = categoria;
            PreRequisito = preRequisito;
            Preco = preco;
            ProfessorId = professorId;
			Aulas = aulas;

			Validate(this, new CursoValidator());
        }

        public Curso(string nome, string descricao, string categoria, string preRequisito, double preco, int aulas)
        {
            Nome = nome;
            Descricao = descricao;
            Categoria = categoria;
            PreRequisito = preRequisito;
            Preco = preco;
			Aulas = aulas;

			Validate(this, new CursoValidator());
        }


        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Categoria { get; set; }
        public string PreRequisito { get; set; }
        public double Preco { get; set; }
		public int Aulas { get; set; }
        public Guid ProfessorId { get; set; }
		public virtual Professor Professor { get; set; }

		public virtual ICollection<CursoProfessor> CursoProfessores { get; set; } = new List<CursoProfessor>();
		public virtual ICollection<UsuarioCurso> UsuarioCursos { get; set; } = new List<UsuarioCurso>();
	}
}
