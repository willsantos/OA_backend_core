using OA_Core.Domain.Validations;

namespace OA_Core.Domain.Entities
{
    public class Usuario : Entidade
    {
        public Usuario(string nome, string email, string senha, DateTime dataNascimento, string telefone, string endereco)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Email = email;
            Senha = senha;
            DataNascimento = dataNascimento;
            Telefone = telefone;
            Endereco = endereco;
            DataCriacao = DateTime.Now;
            Validate(this, new UsuarioValidator());
        }

        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Telefone { get; set; }
        public string Endereco { get; set; }

		public virtual ICollection<UsuarioCurso> UsuarioCursos { get; set; } = new List<UsuarioCurso>();

	}
}
