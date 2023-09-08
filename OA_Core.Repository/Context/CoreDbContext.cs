using Microsoft.EntityFrameworkCore;
using OA_Core.Domain.Entities;

namespace OA_Core.Repository.Context
{
    public class CoreDbContext : DbContext
    {
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Professor> Professor { get; set; }
        public DbSet<Curso> Curso { get; set; }
        public DbSet<Aluno> Aluno { get; set; }

        public CoreDbContext(DbContextOptions<CoreDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().Ignore(u => u.Valid).Ignore(u => u.ValidationResult);
            modelBuilder.Entity<Professor>().Ignore(u => u.Valid).Ignore(u => u.ValidationResult);
            modelBuilder.Entity<Curso>().Ignore(u => u.Valid).Ignore(u => u.ValidationResult);
            modelBuilder.Entity<Aluno>().Ignore(a => a.Valid).Ignore(a => a.ValidationResult);

            modelBuilder.Entity<Curso>().Property(c => c.DataAlteracao).HasColumnName("data_alteracao");
            modelBuilder.Entity<Curso>().Property(c => c.PreRequisito).HasColumnName("pre_requisito");
            modelBuilder.Entity<Curso>().Property(c => c.ProfessorId).HasColumnName("professor_id");
            modelBuilder.Entity<Curso>().Property(c => c.DataCriacao).HasColumnName("data_criacao");
            modelBuilder.Entity<Curso>().Property(c => c.DataDelecao).HasColumnName("data_delecao");


        }

    }
}
