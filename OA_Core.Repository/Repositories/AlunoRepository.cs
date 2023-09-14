using Microsoft.EntityFrameworkCore;
using OA_Core.Domain.Entities;
using OA_Core.Domain.Interfaces.Repository;
using OA_Core.Repository.Context;
using MySqlConnector;

namespace OA_Core.Repository.Repositories
{
    public class AlunoRepository : IAlunoRepository
    {
        private readonly CoreDbContext _context;

        public AlunoRepository(CoreDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Aluno aluno)
        {
            var sql = "INSERT INTO Aluno VALUES(@id, @usuario_id, @foto, @cpf, @data_criacao, @data_alteracao, @data_delecao)";
            object[] paramItems = new object[]
                {
                new MySqlParameter("@id", aluno.Id),
                new MySqlParameter("@usuario_id", aluno.UsuarioId),
                new MySqlParameter("@foto", aluno.Foto),
                new MySqlParameter("@cpf", aluno.Cpf),
                new MySqlParameter("@data_criacao", aluno.DataCriacao),
                new MySqlParameter("@data_alteracao", aluno.DataAlteracao),
                new MySqlParameter("@data_delecao", aluno.DataDelecao)
                };
            await _context.Database.ExecuteSqlRawAsync(sql, paramItems);
        }

        public async Task EditAsync(Aluno aluno)
        {
            var sql = "UPDATE Aluno SET id = @id, usuario_id = @usuario_id, foto = @foto, cpf = @cpf, data_alteracao = @data_alteracao, data_delecao = @data_delecao WHERE id = @id";
            object[] paramItems = new object[]
                {
                new MySqlParameter("@id", aluno.Id),
                new MySqlParameter("@usuario_id", aluno.UsuarioId),
                new MySqlParameter("@foto", aluno.Foto),
                new MySqlParameter("@cpf", aluno.Cpf),
                new MySqlParameter("@data_criacao", aluno.DataCriacao),
                new MySqlParameter("@data_alteracao", aluno.DataAlteracao),
                new MySqlParameter("@data_delecao", aluno.DataDelecao)
                };
            await _context.Database.ExecuteSqlRawAsync(sql, paramItems);
        }

        public async Task<Aluno> FindAsync(Guid id)
        {
            var query = "SELECT id, usuario_id, foto, cpf, data_criacao, data_alteracao, data_delecao" +
                " FROM Aluno WHERE id = @id AND data_delecao IS NULL";
            object[] paramItems = new object[]
          {
                new MySqlParameter("@id", id)
          };
            return await _context.Aluno.FromSqlRaw(query, paramItems).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Aluno>> ListAsync()
        {
            var query = "SELECT id, usuario_id UsuarioId, foto Foto, cpf Cpf, data_criacao DataCriacao, data_alteracao DataAlteracao, data_delecao DataDelecao FROM Aluno WHERE data_delecao IS NULL";
            return await _context.Aluno.FromSqlRaw(query).ToListAsync();
        }

        public async Task<IEnumerable<Aluno>> ListPaginationAsync(int page, int rows)
        {
            var query = string.Format("SELECT u.nome, a.id, a.usuario_id, a.foto, a.cpf, a.data_criacao, a.data_alteracao, a.data_delecao" +
				" FROM Usuario u INNER JOIN Aluno a ON u.id = a.usuario_id AND a.data_delecao is null ORDER BY u.nome LIMIT @limit OFFSET @offset;", page * rows, rows);
            object[] paramItems = new object[]
           {
                new MySqlParameter("@offset", page * rows),
                new MySqlParameter("@limit", rows)
           };

			return await _context.Aluno.FromSqlRaw(query, paramItems).ToListAsync();
        }

        public async Task RemoveAsync(Aluno aluno)
        {
            var sql = "UPDATE Aluno SET data_delecao = @data_delecao WHERE Id = @id";
            object[] paramItems = new object[]
            {
                new MySqlParameter("@id", aluno.Id),
                new MySqlParameter("@data_delecao", aluno.DataDelecao)
            };
            await _context.Database.ExecuteSqlRawAsync(sql, paramItems);
        }

		public async Task<Aluno> FindByCpfAsync(string cpf)
		{
			var query = "SELECT * FROM Aluno a WHERE a.cpf = @cpf";
			object[] paramItems = new object[]
		  {
				new MySqlParameter("@cpf", cpf)
		  };
			return await _context.Aluno.FromSqlRaw(query, paramItems).FirstOrDefaultAsync();
		}
	}
}
