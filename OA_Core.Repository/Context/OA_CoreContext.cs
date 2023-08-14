using Microsoft.EntityFrameworkCore;


namespace OA_Core.Repository.Context
{
    public class OA_CoreContext : DbContext
    {
        public OA_CoreContext(DbContextOptions options) : base(options)
        {
        }
       // public DbSet<Usuario> Usuarios { get; set; }
    }
}
