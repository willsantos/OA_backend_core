using OA_Core.Domain.Entities;
using OA_Core.Domain.Interfaces.Repository;
using OA_Core.Repository.Context;

namespace OA_Core.Repository.Repositories
{
	public class AvaliacaoUsuarioRepository : BaseRepository<AvaliacaoUsuario>, IAvaliacaoUsuarioRepository
	{
		public AvaliacaoUsuarioRepository(CoreDbContext coreDbContext) : base(coreDbContext)
		{
		}
	}
}
