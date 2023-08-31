﻿using Microsoft.EntityFrameworkCore;
using OA_Core.Domain.Entities;
using OA_Core.Domain.Interfaces.Repository;
using OA_Core.Repository.Context;
using MySqlConnector;
namespace OA_Core.Repository.Repositories
{
    public class ProfessorRepository : IProfessorRepository
    {
        private readonly CoreDbContext _context;

        public ProfessorRepository(CoreDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Professor professor)
        {
            var sql = "INSERT INTO Professor VALUES(@id, @usuario_id, @formacao, @experiencia, @foto, @biografia, @data_criacao, @data_alteracao, @data_delecao)";
            object[] paramItems = new object[]
            {
                new MySqlParameter("@id", professor.Id),
                new MySqlParameter("@usuario_id", professor.UsuarioId),
                new MySqlParameter("@formacao", professor.Formacao),
                new MySqlParameter("@experiencia", professor.Experiencia),                
                new MySqlParameter("@foto", professor.Foto),
                new MySqlParameter("@biografia", professor.Biografia),
                new MySqlParameter("@data_criacao", professor.DataCriacao),
                new MySqlParameter("@data_alteracao", professor.DataAlteracao),
                new MySqlParameter("@data_delecao", professor.DataDelecao)

            };
            await _context.Database.ExecuteSqlRawAsync(sql, paramItems);
        }

        public async Task EditAsync(Professor professor)
        {
            var sql = "UPDATE Usuario SET id = @id, usuario_id = @usuario_id, formacao = @formacao, experiencia = @experiencia, foto = @foto, biografia = @biografia, data_criacao = @data_criacao, data_alteracao = @data_alteracao, data_delecao = @data_delecao";
            object[] paramItems = new object[]
            {
                new MySqlParameter("@id", professor.Id),
                new MySqlParameter("@usuario_id", professor.UsuarioId),
                new MySqlParameter("@formacao", professor.Formacao),
                new MySqlParameter("@usuario_id", professor.Experiencia),
                new MySqlParameter("@foto", professor.Foto),
                new MySqlParameter("@usuario_id", professor.Biografia),
                new MySqlParameter("@data_criacao", professor.DataCriacao),
                new MySqlParameter("@data_alteracao", professor.DataAlteracao),
                new MySqlParameter("@data_delecao", professor.DataDelecao)

            };
            await _context.Database.ExecuteSqlRawAsync(sql, paramItems);
        }

        public async Task<Professor> FindAsync(Guid id)
        {
            var query = "SELECT id, usuario_id UsuarioId, formacao, experiencia, foto, biografia, data_criacao DataCriacao, data_alteracao DataAlteracao, data_delecao DataDelecao FROM Professor WHERE id = @id AND data_delecao is null";
            object[] paramItems = new object[]
          {
                new MySqlParameter("@id", id),         
          };

            return await _context.Professor.FromSqlRaw(query, paramItems).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Professor>> ListAsync()
        {
            var query = "SELECT id, usuario_id AS UsuarioId, formacao, experiencia, foto, biografia, data_criacao AS DataCriacao, data_alteracao AS DataAlteracao, data_delecao AS DataDelecao FROM Professor WHERE id = @id AND data_delecao is null";
            return await _context.Professor.FromSqlRaw(query).ToListAsync();

        }

        public async Task<IEnumerable<Professor>> ListPaginationAsync(int page, int rows)
        {
            var query = string.Format("SELECT u.nome, p.formacao, u.id  FROM Usuario u INNER JOIN Professor p ON u.id = p.usuario_id ORDER BY u.nome LIMIT @limit OFFSET @offset;", page * rows, rows);
            object[] paramItems = new object[]
            {
                new MySqlParameter("@offset", page * rows),
                new MySqlParameter("@limit", rows),
            };
            return await _context.Professor.FromSqlRaw(query,paramItems).ToListAsync();
        }

        public async Task RemoveAsync(Professor professor)
        {
            var sql = "UPDATE Professor SET data_delecao = @data_delecao WHERE Id = @id";
            object[] paramItems = new object[]
            {
                new MySqlParameter("@id", professor.Id),
                new MySqlParameter("@data_delecao", professor.DataDelecao),
            };
            await _context.Database.ExecuteSqlRawAsync(sql, paramItems);
        }
    }
}