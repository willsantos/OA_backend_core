namespace OA_Core.Domain.Entities
{
    public class Professor : Entidade
    {
        public string Formacao { get; set; }
        public string Experiencia { get; set; }
        public string Foto { get; set; }
        public string Biografia { get; set; }
        public Guid UsuarioId { get; set; }
    }
}
