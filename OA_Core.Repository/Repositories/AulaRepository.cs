using Microsoft.EntityFrameworkCore;
using OA_Core.Domain.Entities;
using OA_Core.Domain.Interfaces.Repository;
using OA_Core.Repository.Context;
using MySqlConnector;
namespace OA_Core.Repository.Repositories
{
    public class AulaRepository : IAulaRepository
    {
        private readonly CoreDbContext _context;

        public AulaRepository(CoreDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Aula aula)
        {
            var sql = "INSERT INTO Aula VALUES(@id, @nome, @descricao, @caminho, @tipo, @duracao, @ordem, @curso_id, @data_criacao, @data_alteracao, @data_delecao)";
            object[] paramItems = new object[]
            {
                new MySqlParameter("@id", aula.Id),
                new MySqlParameter("@nome", aula.Nome),
                new MySqlParameter("@descricao", aula.Descricao),
                new MySqlParameter("@caminho", aula.Caminho),
                new MySqlParameter("@tipo", aula.Tipo),
                new MySqlParameter("@duracao", aula.Duracao),
                new MySqlParameter("@ordem", aula.Ordem),
                new MySqlParameter("@curso_id", aula.CursoId),
                new MySqlParameter("@data_criacao", aula.DataCriacao),
                new MySqlParameter("@data_alteracao", aula.DataAlteracao),
                new MySqlParameter("@data_delecao", aula.DataDelecao)
            };
            await _context.Database.ExecuteSqlRawAsync(sql, paramItems);
        }

        public async Task EditAsync(Aula aula)
        {
            var sql = "UPDATE Aula SET id = @id, nome = @nome, descricao = @descricao, caminho = @caminho, tipo = @tipo, duracao = @duracao, ordem = @ordem, curso_id = @curso_id, data_criacao = @data_criacao, data_alteracao = @data_alteracao, data_delecao = @data_delecao WHERE id = @id";
            object[] paramItems = new object[]
            {
                new MySqlParameter("@id", aula.Id),
                new MySqlParameter("@nome", aula.Nome),
                new MySqlParameter("@descricao", aula.Descricao),
                new MySqlParameter("@caminho", aula.Caminho),
                new MySqlParameter("@tipo", aula.Tipo),
                new MySqlParameter("@duracao", aula.Duracao),
                new MySqlParameter("@ordem", aula.Ordem),
                new MySqlParameter("@curso_id", aula.CursoId),
                new MySqlParameter("@data_criacao", aula.DataCriacao),
                new MySqlParameter("@data_alteracao", aula.DataAlteracao),
                new MySqlParameter("@data_delecao", aula.DataDelecao)
            };
            await _context.Database.ExecuteSqlRawAsync(sql, paramItems);
        }

        public async Task<Aula> FindAsync(Guid id)
        {
            var query = "SELECT * FROM Aula WHERE id = @id AND data_delecao is null";
            
            object[] paramItems = new object[]
          {
                new MySqlParameter("@id", id),
          };

            return await _context.Aula.FromSqlRaw(query, paramItems).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Aula>> ListAsync()
        {
            var query = "SELECT a.* FROM Curso c INNER JOIN Aula a ON c.id = a.curso_id WHERE a.data_delecao IS NULL ORDER BY a.nome";

            return await _context.Aula.FromSqlRaw(query).ToListAsync();
        }

        public async Task<IEnumerable<Aula>> ListPaginationAsync(int page, int rows)
        {
            var query = string.Format("SELECT a.* FROM Curso c INNER JOIN Aula a ON c.id = a.curso_id WHERE a.data_delecao IS NULL ORDER BY a.nome LIMIT @limit OFFSET @offset;", page * rows, rows);
            object[] paramItems = new object[]
            {
                new MySqlParameter("@offset", page * rows),
                new MySqlParameter("@limit", rows),
            };
            return await _context.Aula.FromSqlRaw(query, paramItems).ToListAsync();
        }

        public async Task RemoveAsync(Aula aula)
        {
            var sql = "UPDATE Aula SET data_delecao = @data_delecao WHERE Id = @id";
            object[] paramItems = new object[]
            {
                new MySqlParameter("@id", aula.Id),
                new MySqlParameter("@data_delecao", aula.DataDelecao),
            };
            await _context.Database.ExecuteSqlRawAsync(sql, paramItems);
        }
    }
}
