namespace OA_Core.Domain.Entities
{
    public class CertificadoAluno
    {
        public Guid CursoId { get; set; }
        public Guid AlunoId { get; set; }
        public DateTime DataEmissao { get; set; }
    }
}
