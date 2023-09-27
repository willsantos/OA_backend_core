using FluentAssertions;
using OA_Core.Domain.Shared;

namespace OA_Core.Tests.Domain.Shared
{
	[Trait("Domain", "Criptografia")]
	public class CriptografiaTests
	{
		[Fact(DisplayName = "Criptografa senha")]
		public void Criptografia_CriptografarSenha_DeveCriptografar()
		{
			// Arrange
			string senha = "MinhaSenhaSecreta";

			// Act
			string senhaCriptografada = Criptografia.CriptografarSenha(senha);

			// Assert
			senhaCriptografada.Should().NotBeNullOrEmpty();
			senhaCriptografada.Should().NotBe(senha);
		}

		[Fact(DisplayName = "Verifica senha")]
		public void Criptografia_TestVerificarSenha_DeveVerificar()
		{
			// Arrange
			string senha = "MinhaSenhaSecreta";
			string senhaCriptografada = Criptografia.CriptografarSenha(senha);

			// Act
			bool senhaCorreta = Criptografia.VerificarSenha(senha, senhaCriptografada);
			bool senhaIncorreta = Criptografia.VerificarSenha("SenhaIncorreta", senhaCriptografada);

			// Assert
			senhaCorreta.Should().BeTrue();
			senhaIncorreta.Should().BeFalse();
		}
	}
}
