namespace OA_Core.Domain.Contracts.Response
{
    public class CursoParaUsuarioResponse : BaseResponse
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Categoria { get; set; }
        public string PreRequisito { get; set; }
        public double Preco { get; set; }
		public int Aulas { get; set; }
		public Guid ProfessorId { get; set; }
		public string Status { get; set; }
		public int Progresso { get; set; }
    }
}
