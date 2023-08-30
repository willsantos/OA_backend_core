namespace OA_Core.Domain.Contracts.Response
{
    public class ProfessorResponse : BaseResponse
    {
        public string Formacao { get; set; }
        public string Experiencia { get; set; }
        public string Foto { get; set; }
        public string Biografia { get; set; }
        public Guid UsuarioId { get; set; }
    }
}
