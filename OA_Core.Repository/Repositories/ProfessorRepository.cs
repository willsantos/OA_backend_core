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
            var sql = "INSERT INTO Professor VALUES(@id,@data_criacao, @data_alteracao, " +
                "                                   @data_delecao, @usuario_id, @formacao, " +
                "                                   @foto, @biografia)";
            await _connection.ExecuteScalarAsync<string>(sql, new
            {
                id = professor.Id,
                data_criacao = professor.DataCriacao,
                data_alteracao = professor.DataAlteracao,
                data_delecao = professor.DataDelecao,
                usuarioid = professor.UsuarioId,
                formacao = professor.Formacao,
                foto = professor.Foto,
                biografia = professor.Biografia
            });
        }

        public async Task EditAsync(Professor professor)
        {
            var sql = "UPDATE Usuario SET id = @id, data_criacao = @data_criacao, " +
                "                         data_alteracao = @data_criacao, " +
                "                         data_delecao = @data_delecao, " +
                "                         usuario_id = @usuario_id, formacao = @formacao, " +
                "                         foto = @foto, biografia = @biografia";
            await _connection.ExecuteAsync(sql, new
            {
                id = professor.Id,
                data_criacao = professor.DataCriacao,
                data_alteracao = professor.DataAlteracao,
                data_delecao = professor.DataDelecao,
                usuarioid = professor.UsuarioId,
                formacao = professor.Formacao,
                foto = professor.Foto,
                biografia = professor.Biografia
            });
        }

        public async Task<Professor> FindAsync(Guid id)
        {
            var query = "SELECT id, data_criacao DataCriacao, " +
                "                   data_alteracao DataAlteracao, " +
                "                   data_delecao DataDelecao, usuario_id UsuarioId, " +
                "                   formacao, foto, biografia FROM Professor WHERE id = @id AND data_delecao is null";
            return await _connection.QueryFirstOrDefaultAsync<Professor>(query, new { id });
        }

        public async Task<IEnumerable<Professor>> ListAsync()
        {
            var query = "SELECT id, data_criacao DataCriacao, " +
                "                   data_alteracao DataAlteracao, " +
                "                   data_delecao DataDelecao, usuario_id UsuarioId, " +
                "                   formacao, foto, biografia FROM Professor WHERE id = @id AND data_delecao is null";
            return await _connection.QueryAsync<Professor>(query);

        }

        public Task<IEnumerable<Professor>> ListPaginationAsync(int page, int rows)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(Professor professor)
        {
            throw new NotImplementedException();
        }
    }
}
