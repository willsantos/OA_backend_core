using Dapper;
using MySql.Data.MySqlClient;
using OA_Core.Domain.Entities;
using OA_Core.Domain.Interfaces.Repository;
using OA_Core.Repository.Context;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_Core.Repository.Repositories
{
    public class AlunoRepository : IAlunoRepository
    {
        private readonly MySqlConnection _connection;

        public AlunoRepository(DapperDbConnection dapperDbConnection)
        {
            _connection = dapperDbConnection.CreateDbConnection() as MySqlConnection;
        }

        public async Task AddAsync(Aluno aluno)
        {
            var sql = "INSERT INTO Aluno VALUES(@id, @UsuarioId, @data_criacao, @data_alteracao, @data_delecao)";
            await _connection.ExecuteScalarAsync<string>(sql, new { id = aluno.Id, data_criacao = aluno.DataCriacao, 
                                                                    data_alteracao = aluno.DataAlteracao, data_delecao = aluno.DataDelecao });
        }

        public async Task EditAsync(Aluno aluno)
        {
            var sql = "UPDATE Aluno SET data_criacao = @data_criacao, data_alteracao = @data_alteracao, data_delecao = @data_delecao WHERE id = @id";
            await _connection.ExecuteAsync(sql, new { data_criacao = aluno.DataCriacao, data_alteracao = aluno.DataAlteracao, data_delecao = aluno.DataDelecao, id = aluno.Id });
        }

        public async Task<Aluno> FindAsync(Guid id)
        {
            var query = "SELECT id, UsuarioId, data_criacao, data_alteracao, data_delecao FROM Aluno WHERE id = @id AND data_delecao IS NULL";
            return await _connection.QueryFirstOrDefaultAsync<Aluno>(query, new { id });
        }

        public async Task<IEnumerable<Aluno>> ListAsync()
        {
            var query = "SELECT id, UsuarioId, data_criacao DataCriacao, data_alteracao DataAlteracao, data_delecao DataDelecao FROM Aluno WHERE data_delecao IS NULL";
            return await _connection.QueryAsync<Aluno>(query);
        }

        public async Task<IEnumerable<Aluno>> ListPaginationAsync(int page, int rows)
        {
            var query = string.Format("SELECT id, UsuarioId, data_criacao DataCriacao, data_alteracao DataAlteracao, data_delecao DataDelecao" +
                " FROM Aluno JOIN Usuario ON Aluno.UsuarioId = Usuario.Id WHERE data_delecao IS NULL ORDER BY nome LIMIT {1} OFFSET {0};", page *rows, rows);
            return await _connection.QueryAsync<Aluno>(query);
        }

        public Task RemoveAsync(Aluno aluno)
        {
            throw new NotImplementedException();
        }
    }
}
