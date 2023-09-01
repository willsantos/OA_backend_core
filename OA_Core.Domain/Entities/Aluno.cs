using OA_Core.Domain.Validations;

namespace OA_Core.Domain.Entities
{
    public class Aluno : Entidade
    {

        public Aluno(Guid usuarioId)
        {
            Id = Guid.NewGuid();
            UsuarioId = usuarioId;
            DataCriacao = DateTime.Now;
            Validate(this, new AlunoValidator());
        }
        public Aluno(string id, string usuarioId, DateTime dataCriacao, DateTime dataAlteracao, DateTime dataDelecao)
        {
            Id = Guid.Parse(id);
            UsuarioId = Guid.Parse(usuarioId);
            DataCriacao = dataCriacao;
            DataAlteracao = dataAlteracao;
            DataDelecao = dataDelecao;
        }

        public Guid UsuarioId { get; set; }
    }
}
