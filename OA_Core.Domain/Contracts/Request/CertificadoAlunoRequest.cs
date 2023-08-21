namespace OA_Core.Domain.Contracts.Request
{
    internal class CertificadoAlunoRequest
    {
        public Guid CursoId { get; set; }
        public Guid AlunoId { get; set; }
        public DateTime DataEmissao { get; set; }
    }
}
