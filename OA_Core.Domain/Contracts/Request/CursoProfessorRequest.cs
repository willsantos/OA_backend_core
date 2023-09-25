namespace OA_Core.Domain.Contracts.Request
{
    public class CursoProfessorRequest
    {
        public Guid ProfessorId { get; set; }
		public Guid CursoId { get; set; }
		public bool Responsavel { get; set; }
    }
}
