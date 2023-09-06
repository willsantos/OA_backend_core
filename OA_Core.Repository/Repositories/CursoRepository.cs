using Microsoft.EntityFrameworkCore;
using OA_Core.Domain.Entities;
using OA_Core.Domain.Interfaces.Repository;
using OA_Core.Repository.Context;
using MySqlConnector;
namespace OA_Core.Repository.Repositories
{
    public class CursoRepository : ICursoRepository
    {
        private readonly CoreDbContext _context;

        public CursoRepository(CoreDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Curso curso)
        {
            var sql = "INSERT INTO Curso VALUES(@id, @nome, @descricao, @categoria, @pre_requisito, @preco, @professor_id, @data_criacao, @data_alteracao, @data_delecao)";
            object[] paramItems = new object[]
            {
                new MySqlParameter("@id", curso.Id),
                new MySqlParameter("@nome", curso.Nome),
                new MySqlParameter("@descricao", curso.Descricao),
                new MySqlParameter("@categoria", curso.Categoria),
                new MySqlParameter("@pre_requisito", curso.PreRequisito),
                new MySqlParameter("@preco", curso.Preco),
                new MySqlParameter("@professor_id", curso.ProfessorId),
                new MySqlParameter("@data_criacao", curso.DataCriacao),
                new MySqlParameter("@data_alteracao", curso.DataAlteracao),
                new MySqlParameter("@data_delecao", curso.DataDelecao)
            };
            await _context.Database.ExecuteSqlRawAsync(sql, paramItems);
        }

        public async Task EditAsync(Curso curso)
        {
            var sql = "UPDATE Curso SET id = @id, nome = @nome, descricao = @descricao, pre_requisito = @pre_requisito, preco = @preco, professor_id = @professor_id, data_criacao = @data_criacao, data_alteracao = @data_alteracao, data_delecao = @data_delecao WHERE id = @id";
            object[] paramItems = new object[]
            {
                new MySqlParameter("@id", curso.Id),
                new MySqlParameter("@nome", curso.Nome),
                new MySqlParameter("@descricao", curso.Descricao),
                new MySqlParameter("@categoria", curso.Categoria),
                new MySqlParameter("@pre_requisito", curso.PreRequisito),
                new MySqlParameter("@preco", curso.Preco),
                new MySqlParameter("@professor_id", curso.ProfessorId),
                new MySqlParameter("@data_criacao", curso.DataCriacao),
                new MySqlParameter("@data_alteracao", curso.DataAlteracao),
                new MySqlParameter("@data_delecao", curso.DataDelecao)
            };
            await _context.Database.ExecuteSqlRawAsync(sql, paramItems);
        }

        public async Task<Curso> FindAsync(Guid id)
        {
            var query = "SELECT id, nome, descricao, categoria, pre_requisito, preco, professor_id, data_criacao, data_alteracao, data_delecao FROM Curso WHERE id = @id AND data_delecao is null";
            
            object[] paramItems = new object[]
          {
                new MySqlParameter("@id", id),
          };

            return await _context.Curso.FromSqlRaw(query, paramItems).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Curso>> ListAsync()
        {
            var query = "SELECT c.id, c.nome, c.descricao, c.categoria, c.pre_requisito, c.preco, c.professor_id, c.data_criacao, c.data_alteracao, c.data_delecao FROM Professor p INNER JOIN Curso c ON p.id = c.professor_id AND c.data_delecao is null ORDER BY c.nome";

            return await _context.Curso.FromSqlRaw(query).ToListAsync();
        }

        public async Task<IEnumerable<Curso>> ListPaginationAsync(int page, int rows)
        {
            var query = string.Format("SELECT c.id, c.nome, c.descricao,c.categoria, c.pre_requisito, c.preco, c.professor_id, c.data_criacao, c.data_alteracao ,c.data_delecao FROM Professor p INNER JOIN Curso c ON p.id = c.professor_id AND c.data_delecao is null ORDER BY c.nome LIMIT @limit OFFSET @offset;", page * rows, rows);
            object[] paramItems = new object[]
            {
                new MySqlParameter("@offset", page * rows),
                new MySqlParameter("@limit", rows),
            };
            return await _context.Curso.FromSqlRaw(query, paramItems).ToListAsync();
        }

        public async Task RemoveAsync(Curso curso)
        {
            var sql = "UPDATE Curso SET data_delecao = @data_delecao WHERE Id = @id";
            object[] paramItems = new object[]
            {
                new MySqlParameter("@id", curso.Id),
                new MySqlParameter("@data_delecao", curso.DataDelecao),
            };
            await _context.Database.ExecuteSqlRawAsync(sql, paramItems);
        }
    }
}
