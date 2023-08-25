namespace OA_Core.Domain.Contracts.Response
{
    public class UsuarioResponse : BaseResponse
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Telefone { get; set; }
        public string Endereco { get; set; }
    }
}
