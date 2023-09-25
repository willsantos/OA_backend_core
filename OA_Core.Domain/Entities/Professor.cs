using OA_Core.Domain.Validations;

namespace OA_Core.Domain.Entities
{
    public class Professor : Entidade
    {
        public Professor(string formacao, string experiencia, string foto, string biografia, Guid usuarioId)
        {
            Id = Guid.NewGuid();
            UsuarioId = usuarioId;
            Formacao = formacao;
            Experiencia = experiencia;
            Foto = foto;
            Biografia = biografia;
            DataCriacao = DateTime.Now;
            Validate(this, new ProfessorValidator());
        }
        public Professor(string formacao, string experiencia, string foto, string biografia)
        {
            Formacao = formacao;
            Experiencia = experiencia;
            Foto = foto;
            Biografia = biografia;
            Validate(this, new ProfessorValidator());
        }

        public Guid UsuarioId { get; set; }
        public string Formacao { get; set; }
        public string Experiencia { get; set; }
        public string Foto { get; set; }
        public string Biografia { get; set; }    
		public virtual Usuario Usuario { get; set; }

		public virtual ICollection<CursoProfessor> CursoProfessores { get; set; } = new List<CursoProfessor>();

	}
}
