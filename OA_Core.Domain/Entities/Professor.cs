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
        public Professor(Guid id, string formacao, string experiencia, string foto, 
                         string biografia, Guid usuarioId, DateTime dataCriacao, DateTime? dataAlteracao, DateTime? dataDelecao)
        {
            Id = id;
            UsuarioId = usuarioId;
            Formacao = formacao;
            Experiencia = experiencia;
            Foto = foto;
            Biografia = biografia;
            DataCriacao = dataCriacao;
            DataAlteracao = dataAlteracao;
            DataDelecao = dataDelecao;
        }
        public Guid UsuarioId { get; set; }
        public string Formacao { get; set; }
        public string Experiencia { get; set; }
        public string Foto { get; set; }
        public string Biografia { get; set; }        
    }
}
