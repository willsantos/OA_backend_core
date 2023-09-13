using OA_Core.Domain.Validations;

namespace OA_Core.Domain.Entities
{
    public class Aluno : Entidade
    {

        public Aluno(Guid usuarioId, string foto, string cpf)
        {
            Id = Guid.NewGuid();
            UsuarioId = usuarioId;
			Foto = foto;
			Cpf = cpf;
            DataCriacao = DateTime.Now;
            Validate(this, new AlunoValidator());
        }
        public Aluno(string foto, string cpf, DateTime dataCriacao, DateTime dataAlteracao, DateTime dataDelecao)
        {
			Foto = foto; 
			Cpf = cpf;
            DataCriacao = dataCriacao;
            DataAlteracao = dataAlteracao;
            DataDelecao = dataDelecao;
        }

        public Guid UsuarioId { get; set; }
		public string Foto { get; set; }
		public string Cpf { get; set; }
    }
}
