using Bogus;
using Bogus.Extensions.Brazil;
using OA_Core.Domain.Entities;

namespace OA_Core.Tests.Config
{
	[CollectionDefinition(nameof(AlunoBogusCollection))]
	public class AlunoBogusCollection : ICollectionFixture<AlunoFixture>
	{ }
	public class AlunoFixture : IDisposable
	{

		public static Aluno GerarAluno()
		{
			var aluno = GerarAlunos(1, true).FirstOrDefault();

			return aluno is null ? throw new Exception("Falha ao gerar aluno com Bogus") : aluno;
		}

		public static IEnumerable<Aluno> GerarAlunos(int quantidade, bool ativo)
		{
			var alunos = new Faker<Aluno>("pt_BR")
				.CustomInstantiator(prop => new Aluno(
					Guid.NewGuid(),
					prop.Name.FirstName(),
					prop.Person.Cpf(false)));

			return alunos.Generate(quantidade);

		}
		public void Dispose()
		{

		}
	}
}
