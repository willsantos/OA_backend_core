using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using OA_Core.Domain.Entities;
using OA_Core.Domain.Interfaces.Repository;
using OA_Core.Repository.Context;


namespace OA_Core.Repository.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly CoreDbContext _context;

        public UsuarioRepository(CoreDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Usuario usuario)
        {

            object[] paramItems = new object[]
            {
                new MySqlParameter("@id", usuario.Id),
                new MySqlParameter("@nome", usuario.Nome),
                new MySqlParameter("@email", usuario.Email),
                new MySqlParameter("@senha", usuario.Senha),
                new MySqlParameter("@data_nascimento", usuario.DataNascimento),
                new MySqlParameter("@telefone", usuario.Telefone),
                new MySqlParameter("@endereco", usuario.Endereco),
                new MySqlParameter("@data_criacao", usuario.DataCriacao),
                new MySqlParameter("@data_alteracao", usuario.DataAlteracao),
                new MySqlParameter("@data_delecao", usuario.DataDelecao),
            };
            await _context.Database.ExecuteSqlRawAsync("INSERT INTO Usuario VALUES(@id, @nome, @email, @senha, @data_nascimento, @telefone, @endereco, @data_criacao, @data_alteracao, @data_delecao)", paramItems);
        }

        public async Task EditAsync(Usuario usuario)
        {
            object[] paramItems = new object[]
            {
                new MySqlParameter("@id", usuario.Id),
                new MySqlParameter("@nome", usuario.Nome),
                new MySqlParameter("@email", usuario.Email),
                new MySqlParameter("@senha", usuario.Senha),
                new MySqlParameter("@data_nascimento", usuario.DataNascimento),
                new MySqlParameter("@telefone", usuario.Telefone),
                new MySqlParameter("@endereco", usuario.Endereco),
                new MySqlParameter("@data_criacao", usuario.DataCriacao),
                new MySqlParameter("@data_alteracao", usuario.DataAlteracao),
                new MySqlParameter("@data_delecao", usuario.DataDelecao),
            };
            await _context.Database.ExecuteSqlRawAsync("UPDATE Usuario SET nome = @nome, email = @email, senha = @senha, data_nascimento = @data_nascimento, telefone = @telefone, endereco = @endereco, data_alteracao = @data_alteracao WHERE id = @id", paramItems);
        }

        public async Task<Usuario> FindAsync(Guid id)
        {
            object[] paramItems = new object[]
            {
                new MySqlParameter("@id", id),
            };
            return await _context.Usuario.FromSqlRaw("SELECT id, nome, email, senha, data_nascimento DataNascimento, telefone, endereco, data_criacao DataCriacao, data_alteracao DataAlteracao, data_delecao DataDelecao FROM Usuario WHERE id = @id AND data_delecao IS NULL", paramItems).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Usuario>> ListAsync()
        {
            return await _context.Usuario.FromSqlRaw("SELECT id, nome, email, senha, data_nascimento DataNascimento, telefone, endereco, data_criacao DataCriacao, data_alteracao DataAlteracao, data_delecao DataDelecao FROM Usuario WHERE data_delecao IS NULL").ToListAsync();
        }

        public async Task<IEnumerable<Usuario>> ListPaginationAsync(int page, int rows)
        {

            object[] paramItems = new object[]
            {
                new MySqlParameter("@offset", page * rows),
                new MySqlParameter("@limit", rows),
            };
            return await _context.Usuario.FromSqlRaw("SELECT id, nome, email, senha, data_nascimento DataNascimento, telefone, endereco, data_criacao DataCriacao, data_alteracao DataAlteracao, data_delecao DataDelecao FROM Usuario WHERE data_delecao IS NULL ORDER BY nome LIMIT @limit OFFSET @offset;", paramItems).ToListAsync();
        }

        public async Task RemoveAsync(Usuario usuario)
        {
            object[] paramItems = new object[]
            {
                new MySqlParameter("@id", usuario.Id),
                new MySqlParameter("@data_delecao", usuario.DataDelecao),
            };
            await _context.Database.ExecuteSqlRawAsync("UPDATE Usuario SET data_delecao = @data_delecao WHERE Id = @id", paramItems);
        }
    }
}
