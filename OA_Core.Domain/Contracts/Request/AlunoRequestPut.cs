using OA_Core.Domain.ValueObjects;

namespace OA_Core.Domain.Contracts.Request
{
    public class AlunoRequestPut
    {
		public string Foto { get; set; }
		public Cpf Cpf { get; set; }
	}
}
