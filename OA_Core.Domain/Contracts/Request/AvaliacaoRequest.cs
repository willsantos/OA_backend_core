using OA_Core.Domain.Enums;

namespace OA_Core.Domain.Contracts.Request
{
    public class AvaliacaoRequest
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public AvaliacaoTipoEnum Tipo { get; set; }
        public double NotaMaxima { get; set; }
        public double NotaMinima { get; set; }
        public DateTime DataEntrega { get; set; }
        public Guid AulaId { get; set; }
    }
}
