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
        public Aluno(DateTime dataCriacao, DateTime dataAlteracao, DateTime dataDelecao)
        {
            DataCriacao = dataCriacao;
            DataAlteracao = dataAlteracao;
            DataDelecao = dataDelecao;
        }

        public Guid UsuarioId { get; set; }
    }
}
