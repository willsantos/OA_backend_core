﻿using Microsoft.EntityFrameworkCore;
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
		public DbSet<Assinatura> Assinatura { get; set; }
		public DbSet<CursoProfessor> CursoProfessor { get; set; }
		public DbSet<UsuarioCurso> UsuarioCurso { get; set; }
		public DbSet<Avaliacao> Avaliacao { get; set; }
		public DbSet<AvaliacaoUsuario> AvaliacaoUsuario { get; set; }

		public CoreDbContext(DbContextOptions<CoreDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			modelBuilder.Entity<Usuario>(new UsuarioEntityMap().Configure);
			modelBuilder.Entity<Professor>(new ProfessorEntityMap().Configure);
			modelBuilder.Entity<Curso>(new CursoEntityMap().Configure);
			modelBuilder.Entity<Aluno>(new AlunoEntityMap().Configure);
			modelBuilder.Entity<Aula>(new AulaEntityMap().Configure);
			modelBuilder.Entity<CursoProfessor>(new CursoProfessorEntityMap().Configure);
			modelBuilder.Entity<UsuarioCurso>(new UsuarioCursoEntityMap().Configure);
			modelBuilder.Entity<Assinatura>(new AssinaturaEntityMap().Configure);
			modelBuilder.Entity<Avaliacao>(new AvaliacaoEntityMap().Configure);
			modelBuilder.Entity<AvaliacaoUsuario>(new AvaliacaoUsuarioEntityMap().Configure);
		}
		public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			foreach (var entry in ChangeTracker.Entries())
			{
				if (entry.Entity is Entidade baseEntity)
				{
					switch (entry.State)
					{
						case EntityState.Added:
							baseEntity.DataCriacao = DateTime.Now;
							break;

						case EntityState.Modified:
							entry.Property("DataCriacao").IsModified = false;
							baseEntity.DataAlteracao = DateTime.Now;
							break;
					}
				}
			}

			return await base.SaveChangesAsync(cancellationToken);
		}
	}
}
