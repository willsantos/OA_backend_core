namespace OA_Core.Domain.Contracts.Request
{
    public class ResultadoRequest
    {
        public Guid AvaliacaoId { get; set; }
        public Guid AlunoId { get; set; }
        public double Nota { get; set; }
    }
}
