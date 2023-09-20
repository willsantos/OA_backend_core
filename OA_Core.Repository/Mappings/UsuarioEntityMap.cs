using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OA_Core.Domain.Entities;

namespace OA_Core.Repository.Mappings
{
	public class UsuarioEntityMap
	{
		public void Configure(EntityTypeBuilder<Usuario> builder)
		{
			builder.Ignore(c => c.Valid).Ignore(c => c.ValidationResult);
			builder.Property(c => c.Nome).HasColumnName("nome");
			builder.Property(c => c.Email).HasColumnName("email");
			builder.Property(c => c.Senha).HasColumnName("senha");
			builder.Property(c => c.DataNascimento).HasColumnName("data_nascimento");
			builder.Property(c => c.Telefone).HasColumnName("telefone");
			builder.Property(c => c.Endereco).HasColumnName("endereco");
			builder.Property(c => c.DataCriacao).HasColumnName("data_criacao");
			builder.Property(c => c.DataAlteracao).HasColumnName("data_alteracao");
			builder.Property(c => c.DataDelecao).HasColumnName("data_delecao");
		}
	}
}
