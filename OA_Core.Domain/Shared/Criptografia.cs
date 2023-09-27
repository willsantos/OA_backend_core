namespace OA_Core.Domain.Shared
{
	public static class Criptografia
	{
		public static string CriptografarSenha(string senha)
		{
			string salt = BC.GenerateSalt(12); 
			string senhaCriptografada = BC.HashPassword(senha, salt);
			return senhaCriptografada;
		}

		public static bool VerificarSenha(string senha, string senhaHash)
		{
			return BC.Verify(senha, senhaHash);
		}
	}
}
