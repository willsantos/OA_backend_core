using Bogus;
using OA_Core.Domain.Contracts.Request;
using OA_Core.Domain.Entities;

namespace OA_Core.Tests.Config
{

	[CollectionDefinition(nameof(UsuarioBogusCollection))]
	public class UsuarioBogusCollection : ICollectionFixture<UsuarioFixture>
	{ }

	public class UsuarioFixture : IDisposable
	{

		public Usuario GerarUsuario()
		{
			var usuario = GerarUsuarios(1, true).FirstOrDefault();

			return usuario is null ? throw new Exception("Falha ao gerar usuario com Bogus") : usuario;
		}

		public UsuarioRequest GerarUsuarioRequest()
		{
			var usuario = GerarUsuarios(1, true).FirstOrDefault() ?? throw new Exception("Falha ao gerar usuario com Bogus");

			var usuarioRequest = new UsuarioRequest
			{
				Nome = usuario.Nome,
				Email = usuario.Email,
				Senha = usuario.Senha,
				DataNascimento = usuario.DataNascimento,
				Telefone = usuario.Telefone,
				Endereco = usuario.Endereco

			};

			return usuarioRequest;

		}

		public IEnumerable<Usuario> GerarUsuarios(int quantidade, bool ativo)
		{
			var usuarios = new Faker<Usuario>("pt_BR")
				.CustomInstantiator(prop => new Usuario(
					prop.Name.FirstName(),
					prop.Internet.Email(),
					prop.Internet.Password(),
					prop.Date.Past(80, DateTime.Now.AddYears(-18)),
					prop.Phone.PhoneNumber(),
					prop.Address.StreetName()));


			return usuarios.Generate(quantidade);

		}

		public void Dispose()
		{
			
		}
	}
}