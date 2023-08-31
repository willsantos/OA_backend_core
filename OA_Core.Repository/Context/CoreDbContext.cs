using Microsoft.EntityFrameworkCore;
using OA_Core.Domain.Entities;

namespace OA_Core.Repository.Context
{
    public class CoreDbContext : DbContext
    {
        public DbSet<Usuario> Usuario { get; set; }

        public CoreDbContext(DbContextOptions<CoreDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().Ignore(u => u.Valid).Ignore(u => u.ValidationResult);
        }

    }
}
