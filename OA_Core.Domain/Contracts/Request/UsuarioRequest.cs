namespace OA_Core.Domain.Contracts.Request
{
    public class UsuarioRequest
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public DateOnly DataNascimento { get; set; }
        public string Telofone { get; set; }
        public string Endereco { get; set; }
    }
}
