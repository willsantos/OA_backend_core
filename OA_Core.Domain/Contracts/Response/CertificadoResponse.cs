namespace OA_Core.Domain.Contracts.Response
{
    public class CertificadoResponse : BaseResponse
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int CargaHoraria { get; set; }
        public byte[] Assinatura { get; set; }
        public string SeloCaminho { get; set; }
    }
}
