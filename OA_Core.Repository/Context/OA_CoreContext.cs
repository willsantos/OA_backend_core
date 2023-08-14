using Microsoft.EntityFrameworkCore;
using OA_Core.Domain.Entities;

namespace OA_Core.Repository.Context
{
    public class OA_CoreContext : DbContext
    {
        public OA_CoreContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Usuario> Usuario { get; set; }
    }
}
