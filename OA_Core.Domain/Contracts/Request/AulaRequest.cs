using OA_Core.Domain.Enums;

namespace OA_Core.Domain.Contracts.Request
{
    public class AulaRequest
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Caminho { get; set; }
        public TipoAula Tipo { get; set; }
        public int Duracao { get; set; }
        public int Ordem { get; set; }
        public Guid CursoId { get; set; }
    }
}
