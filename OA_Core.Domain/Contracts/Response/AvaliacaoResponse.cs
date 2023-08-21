using OA_Core.Domain.Enums;

namespace OA_Core.Domain.Contracts.Response
{
    public class AvaliacaoResponse : BaseResponse
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public TipoAvaliacao Tipo { get; set; }
        public double NotaMaxima { get; set; }
        public double NotaMinima { get; set; }
        public DateTime DataEntrega { get; set; }
        public Guid AulaId { get; set; }
    }
}
