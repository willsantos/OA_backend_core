using OA_Core.Domain.Enums;
using OA_Core.Domain.Validations;

namespace OA_Core.Domain.Entities
{
    public class Aula : Entidade
    {
        public Aula(string nome, string descricao, string caminho, string tipo, int duracao, int ordem, Guid cursoId)
        { 
            Id = Guid.NewGuid();
            Nome = nome;
            Descricao = descricao;
            Caminho = caminho;
            Tipo = tipo;
            Duracao = duracao;
            Ordem = ordem;
            CursoId = cursoId;
            DataCriacao = DateTime.Now;
            Validate(this, new AulaValidator());
        }

        public Aula(string nome, string descricao, string caminho, string tipo, int duracao, int ordem)
        {
            Nome = nome;
            Descricao = descricao;
            Caminho = caminho;
            Tipo = tipo;
            Duracao = duracao;
            Ordem = ordem;
            Validate(this, new AulaValidator());
        }

        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Caminho { get; set; }
        public string Tipo { get; set; }
        public int Duracao { get; set; }
        public int Ordem { get; set; }
        public Guid CursoId { get; set; }
    }
}
