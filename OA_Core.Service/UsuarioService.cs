using OA_Core.Domain.Entities;
using OA_Core.Domain.Interfaces.Repository;
using OA_Core.Domain.Interfaces.Service;

namespace OA_Core.Service
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioService(IUsuarioRepository repository)
        {
            _repository = repository;
        }
        public async Task DeleteUsuarioAsync(Guid id)
        {
            var usuario = await _repository.FindAsync(id);
            //Fazer essa verificacão no back ou na query - especificar que nao quero trazer dados onde a data de delecao existe e vericar se o resultado é nulo
            if (usuario.DataDelecao != null)
            {
                throw new Exception("Usuario já foi deletado");
            }
            usuario.DataDelecao = DateTime.Now;
            await _repository.RemoveAsync(usuario);
        }

        public async Task<IEnumerable<Usuario>> GetAllUsuariosAsync(int pages, int rows)
        {
            return await _repository.ListPaginationAsync(pages, rows);
        }

        public async Task<Usuario> GetUsuarioByIdAsync(Guid id)
        {
            var usuario = await _repository.FindAsync(id);
            ArgumentNullException.ThrowIfNull(usuario);
            if (usuario.DataDelecao != null)
            {
                throw new Exception("Usuario já foi deletado");
            }
            return usuario;
        }

        public async Task<Guid> PostUsuarioAsync(Usuario usuario)
        {
            //Encryptar senha
            //Mandar email confirmacao usuario
            usuario.Id = Guid.NewGuid();
            usuario.DataCriacao = DateTime.Now;
            await _repository.AddAsync(usuario);
            return usuario.Id;
        }

        public async Task PutUsuarioAsync(Guid id, Usuario usuario)
        {
            //fazer verificacoes de seguranca para permitir a edicao a partir de quem estiver logado no sistema
            var entity = await _repository.FindAsync(id);
            ArgumentNullException.ThrowIfNull(usuario);
            if (entity.DataDelecao != null)
            {
                throw new Exception("Usuario já foi deletado");
            }
            usuario.Id = entity.Id;
            usuario.DataCriacao = entity.DataCriacao;
            usuario.DataAlteracao = DateTime.Now;
            await _repository.EditAsync(usuario);
        }
    }
}
