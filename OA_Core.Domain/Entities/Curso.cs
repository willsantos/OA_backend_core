using OA_Core.Domain.Validations;

namespace OA_Core.Domain.Entities
{
    public class Curso : Entidade
    {
        //public Curso()
        //{
        //    Id = Guid.NewGuid();
        //    Validate(this, new CursoValidator());
        //}

        public Curso(string nome, string descricao, string categoria, string preRequisito, double preco, Guid professorId)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Descricao = descricao;
            Categoria = categoria;
            PreRequisito = preRequisito;
            Preco = preco;
            ProfessorId = professorId;
            DataCriacao = DateTime.Now;
            Validate(this, new CursoValidator());
        }

        public Curso(string nome, string descricao, string categoria, string preRequisito, double preco)
        {
            Nome = nome;
            Descricao = descricao;
            Categoria = categoria;
            PreRequisito = preRequisito;
            Preco = preco;
            Validate(this, new CursoValidator());
        }


        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Categoria { get; set; }
        public string PreRequisito { get; set; }
        public double Preco { get; set; }
        public Guid ProfessorId { get; set; }
    }
}
