using Microsoft.EntityFrameworkCore;
using OA_Core.Domain.Entities;
using OA_Core.Domain.Interfaces.Repository;
using OA_Core.Repository.Context;
using MySqlConnector;

namespace OA_Core.Repository.Repositories
{
    public class AlunoRepository : BaseRepository<Aluno>, IAlunoRepository
    {
        private readonly CoreDbContext _context;

        public AlunoRepository(CoreDbContext context) : base(context)
        {
            _context = context;
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
