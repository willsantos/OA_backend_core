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
            Validate(this, new AlunoValidator());
        }
        public Aluno(string foto, string cpf)
        {
			Foto = foto; 
			Cpf = cpf;
			Validate(this, new AlunoValidator());
		}

        public Guid UsuarioId { get; set; }
		public string Foto { get; set; }
		public string Cpf { get; set; }

		public virtual Usuario usuario { get; set; }
    }
}
