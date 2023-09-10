using Microsoft.EntityFrameworkCore;
using OA_Core.Domain.Entities;
using OA_Core.Repository.Mappings;

namespace OA_Core.Repository.Context
{
    public class CoreDbContext : DbContext
    {
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Professor> Professor { get; set; }
        public DbSet<Curso> Curso { get; set; }
        public DbSet<Aluno> Aluno { get; set; }
        public DbSet<Aula> Aula { get; set; }

        public CoreDbContext(DbContextOptions<CoreDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().Ignore(u => u.Valid).Ignore(u => u.ValidationResult);
            modelBuilder.Entity<Professor>().Ignore(p => p.Valid).Ignore(p => p.ValidationResult);
            modelBuilder.Entity<Aluno>().Ignore(a => a.Valid).Ignore(a => a.ValidationResult);
            modelBuilder.Entity<Aula>().Ignore(a => a.Valid).Ignore(a => a.ValidationResult);

            modelBuilder.Entity<Curso>(new CursoEntityMap().Configure);
        }
    }
}
