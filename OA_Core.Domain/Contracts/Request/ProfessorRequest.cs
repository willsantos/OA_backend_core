namespace OA_Core.Domain.Contracts.Request
{
    public class ProfessorRequest
    {
        public string Formacao { get; set; }
        public string Experiencia { get; set; }
        public string Foto { get; set; }
        public string Biografia { get; set; }
        public Guid UsuarioId { get; set; }
    }
}
