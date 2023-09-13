namespace OA_Core.Domain.Contracts.Request
{
    public class AlunoRequest
    {
        public Guid UsuarioId { get; set; }
		public string Foto { get; set; }
		public string Cpf { get; set; }
    }
}
