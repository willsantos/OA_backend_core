using OA_Core.Domain.Entities;
using OA_Core.Domain.Interfaces.Repository;
using OA_Core.Repository.Context;


namespace OA_Core.Repository.Repositories
{
	public class AssinaturaRepository : BaseRepository<Assinatura>, IAssinaturaRepository
	{
		private readonly CoreDbContext _context;
		public AssinaturaRepository(CoreDbContext context) : base(context)
		{
			_context = context;
		}
	}
}
