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

        public Usuario(Guid id, string nome, string email, string senha, DateTime dataNascimento, string telefone, string endereco, DateTime dataCriacao, DateTime dataAlteracao, DateTime dataDelecao)
        {
            Id = id;
            Nome = nome;
            Email = email;
            Senha = senha;
            DataNascimento = dataNascimento;
            Telefone = telefone;
            Endereco = endereco;
            DataCriacao = dataCriacao;
            DataAlteracao = dataAlteracao;
            DataDelecao = dataDelecao;
        }

        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Telefone { get; set; }
        public string Endereco { get; set; }
    }
}
