using Dapper;
using MySql.Data.MySqlClient;
using OA_Core.Domain.Entities;
using OA_Core.Domain.Interfaces.Repository;
using OA_Core.Repository.Context;

namespace OA_Core.Repository.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly MySqlConnection _connection;

        public UsuarioRepository(DapperDbConnection dapperDbConnection)
        {
            _connection = dapperDbConnection.CreateDbConnection() as MySqlConnection;

        }
        public async Task AddAsync(Usuario usuario)
        {
            var sql = "INSERT INTO Usuario VALUES(@id, @nome, @email, @senha, @data_nascimento, @telefone, @endereco, @data_criacao, @data_alteracao, @data_delecao)";
            await _connection.ExecuteScalarAsync<string>(sql, new { id = usuario.Id, nome = usuario.Nome, email = usuario.Email, senha = usuario.Senha, data_nascimento = usuario.DataNascimento, telefone = usuario.Telefone, endereco = usuario.Endereco, data_criacao = usuario.DataCriacao, data_alteracao = usuario.DataAlteracao, data_delecao = usuario.DataDelecao});
        }

        public async Task EditAsync(Usuario usuario)
        {
            var sql = "UPDATE Usuario SET nome = @nome, email = @email, senha = @senha, data_nascimento = @data_nascimento, telefone = @telefone, endereco = @endereco, data_alteracao = @data_alteracao WHERE id = @id";
            await _connection.ExecuteAsync(sql, new { nome = usuario.Nome, email = usuario.Email, senha = usuario.Senha, data_nascimento = usuario.DataNascimento, telefone = usuario.Telefone, endereco = usuario.Endereco, data_alteracao = usuario.DataAlteracao, id = usuario.Id});
        }

        public async Task<Usuario> FindAsync(Guid id)
        {
            var query = "SELECT id, nome, email, senha, data_nascimento DataNascimento, telefone, endereco, data_criacao DataCriacao, data_alteracao DataAlteracao, data_delecao DataDelecao FROM Usuario WHERE id = @id AND data_delecao IS NULL";
            return await _connection.QueryFirstOrDefaultAsync<Usuario>(query, new { id });
        }

        public async Task<IEnumerable<Usuario>> ListAsync()
        {
            var query = "SELECT id, nome, email, senha, data_nascimento DataNascimento, telefone, endereco, data_criacao DataCriacao, data_alteracao DataAlteracao, data_delecao DataDelecao FROM Usuario WHERE data_delecao IS NULL";
            return await _connection.QueryAsync<Usuario>(query);
        }

        public async Task<IEnumerable<Usuario>> ListPaginationAsync(int page, int rows)
        {
            var query = string.Format("SELECT id, nome, email, senha, data_nascimento DataNascimento, telefone, endereco, data_criacao DataCriacao, data_alteracao DataAlteracao, data_delecao DataDelecao FROM Usuario WHERE data_delecao IS NULL ORDER BY nome LIMIT {1} OFFSET {0};", page * rows, rows);
            return await _connection.QueryAsync<Usuario>(query);
        }

        public async Task RemoveAsync(Usuario usuario)
        {
            var sql = "UPDATE Usuario SET data_delecao = @data_delecao WHERE Id = @id";
            await _connection.ExecuteAsync(sql, new { data_delecao = usuario.DataDelecao, id = usuario.Id });

        }
    }
}
