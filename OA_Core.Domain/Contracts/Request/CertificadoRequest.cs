namespace OA_Core.Domain.Contracts.Request
{
    public class CertificadoRequest
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int CargaHoraria { get; set; }
        public byte[] Assinatura { get; set; }
        public string SeloCaminho { get; set; }
    }
}
