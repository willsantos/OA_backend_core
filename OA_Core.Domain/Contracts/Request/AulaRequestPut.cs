namespace OA_Core.Domain.Contracts.Request
{
    public class AulaRequestPut
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Caminho { get; set; }
        public string Tipo { get; set; }
        public int Duracao { get; set; }
        public int Ordem { get; set; }
    }
}
