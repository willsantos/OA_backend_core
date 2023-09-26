using OA_Core.Domain.Enums;

namespace OA_Core.Domain.Contracts.Request
{
    public class UsuarioCursoRequest
	{
        public Guid CursoId { get; set; }
		public StatusUsuarioCursoEnum Status { get; set; }
		public int Progresso { get; set; }

    }
}
