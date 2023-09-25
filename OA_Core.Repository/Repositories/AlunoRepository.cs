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
	}
}
