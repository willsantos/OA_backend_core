namespace OA_Core.Domain.Contracts.Request
{
    public class CursoRequestPut
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Categoria { get; set; }
        public string PreRequisito { get; set; }
        public double Preco { get; set; }
		public int Aulas { get; set; }

	}
}
