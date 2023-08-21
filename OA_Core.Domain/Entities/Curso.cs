namespace OA_Core.Domain.Entities
{
    public class Curso : Entidade
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Categoria { get; set; }
        public string PreRequesito { get; set; }
        public double Preco { get; set; }
        public Guid ProfessorId { get; set; }
    }
}
