using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using OA_Core.Domain.Entities;
using OA_Core.Domain.Enums;
using OA_Core.Domain.Exceptions;
using OA_Core.Domain.Interfaces.Repository;
using OA_Core.Repository.Context;


namespace OA_Core.Repository.Repositories
{
	public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
	{
		public UsuarioRepository(CoreDbContext coreDbContext) : base(coreDbContext)
		{
		}
	}
}
