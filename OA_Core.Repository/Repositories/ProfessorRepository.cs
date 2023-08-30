using Dapper;
using MySql.Data.MySqlClient;
using OA_Core.Domain.Entities;
using OA_Core.Domain.Interfaces.Repository;
using OA_Core.Repository.Context;

namespace OA_Core.Repository.Repositories
{
    public class ProfessorRepository : IProfessorRepository
    {
        private readonly MySqlConnection _connection;

        public ProfessorRepository(DapperDbConnection dapperDbConnection)
        {
            _connection = dapperDbConnection.CreateDbConnection() as MySqlConnection;
        }

        public async Task AddAsync(Professor professor)
        {
            var sql = "INSERT INTO Professor VALUES(@id, @usuario_id, @formacao, @experiencia, @foto, @biografia, @data_criacao, @data_alteracao, @data_delecao)";
            await _connection.ExecuteScalarAsync<string>(sql, new
            {
                id = professor.Id,
                usuario_id = professor.UsuarioId,
                formacao = professor.Formacao,
                experiencia = professor.Experiencia,
                foto = professor.Foto,
                biografia = professor.Biografia,
                data_criacao = professor.DataCriacao,
                data_alteracao = professor.DataAlteracao,
                data_delecao = professor.DataDelecao                       
                                
            });
        }

        public async Task EditAsync(Professor professor)
        {
            var sql = "UPDATE Usuario SET id = @id, usuario_id = @usuario_id, formacao = @formacao, experiencia = @experiencia, foto = @foto, biografia = @biografia, data_criacao = @data_criacao, data_alteracao = @data_alteracao, data_delecao = @data_delecao";
            await _connection.ExecuteAsync(sql, new
            {
                id = professor.Id,
                usuario_id = professor.UsuarioId,
                formacao = professor.Formacao,
                experiencia = professor.Experiencia,
                foto = professor.Foto,
                biografia = professor.Biografia,
                data_criacao = professor.DataCriacao,
                data_alteracao = professor.DataAlteracao,
                data_delecao = professor.DataDelecao
            });
        }

        public async Task<Professor> FindAsync(Guid id)
        {
            var query = "SELECT id, usuario_id UsuarioId, formacao, experiencia, foto, biografia, data_criacao DataCriacao, data_alteracao DataAlteracao, data_delecao DataDelecao FROM Professor WHERE id = @id AND data_delecao is null";
            return await _connection.QueryFirstOrDefaultAsync<Professor>(query, new { id });
        }

        public async Task<IEnumerable<Professor>> ListAsync()
        {
            var query = "SELECT id, usuario_id AS UsuarioId, formacao, experiencia, foto, biografia, data_criacao AS DataCriacao, data_alteracao AS DataAlteracao, data_delecao AS DataDelecao FROM Professor WHERE id = @id AND data_delecao is null";
            return await _connection.QueryAsync<Professor>(query);

        }

        public async Task<IEnumerable<Professor>> ListPaginationAsync(int page, int rows)
        {
            var query = string.Format("SELECT id, usuario_id AS UsuarioId, formacao, experiencia, foto, biografia, data_criacao AS DataCriacao, data_alteracao AS DataAlteracao, data_delecao AS DataDelecao FROM Professor JOIN Usuario ON Professor.UsuarioId = usuario.id WHERE data_delecao is nulL ORDER BY usuario.nome LIMIT {1} OFFSET {0};", page* rows, rows);
            return await _connection.QueryAsync<Professor>(query);
        }

        public async Task RemoveAsync(Professor professor)
        {
            var sql = "UPDATE Professor SET data_delecao = @data_delecao WHERE Id = @id";
            await _connection.ExecuteAsync(sql, new { data_delecao = professor.DataDelecao, id = professor.Id });           
        }
    }
}
