using Microsoft.EntityFrameworkCore;
using OA_Core.Domain.Entities;
using OA_Core.Domain.Interfaces.Repository;
using OA_Core.Repository.Context;
using MySqlConnector;
namespace OA_Core.Repository.Repositories
{
	public class AulaRepository : BaseRepository<Aula>, IAulaRepository
	{
		private readonly CoreDbContext _context;

		public AulaRepository(CoreDbContext coreDbContext) : base(coreDbContext)
		{
			_context = coreDbContext;
		}
	}
}
