using System.Security.Cryptography;
using System.Text;

namespace OA_Core.Domain.Shared
{
	public class Criptografia
	{
		public static string Encrypt(string entryText, byte[] salt)
		{
			byte[] Key = new byte[]
			{
			12, 2, 56, 117, 12, 67, 33, 23,
			12, 2, 56, 117, 12, 67, 33, 23,
			12, 2, 56, 117, 12, 67, 33, 23,
			12, 2, 56, 117, 12, 67, 33, 23
			};

			Aes Algorithm = Aes.Create();
			Algorithm.KeySize = 256;

			byte[] iniVetor = new byte[]
			{
			1, 2, 3, 4, 5, 6, 7, 8,
			9, 10, 11, 12, 13, 14, 15, 16
			};

			byte[] combinedKey = new byte[Key.Length + salt.Length];
			Buffer.BlockCopy(Key, 0, combinedKey, 0, Key.Length);
			Buffer.BlockCopy(salt, 0, combinedKey, Key.Length, salt.Length);

			byte[] symEncryptedData;

			var dataToProtectAsArray = Encoding.UTF8.GetBytes(entryText);
			using (var encryptor = Algorithm.CreateEncryptor(combinedKey, iniVetor))
			using (var memoryStream = new MemoryStream())
			using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
			{
				cryptoStream.Write(dataToProtectAsArray, 0, dataToProtectAsArray.Length);
				cryptoStream.FlushFinalBlock();
				symEncryptedData = memoryStream.ToArray();
			}
			Algorithm.Dispose();
			return Convert.ToBase64String(symEncryptedData);
		}

		public static string Decrypt(string entryText, byte[] salt)
		{
			byte[] Key = new byte[]
			{
			12, 2, 56, 117, 12, 67, 33, 23,
			12, 2, 56, 117, 12, 67, 33, 23,
			12, 2, 56, 117, 12, 67, 33, 23,
			12, 2, 56, 117, 12, 67, 33, 23
			};

			Aes Algorithm = Aes.Create();
			Algorithm.KeySize = 256;

			byte[] iniVetor = new byte[]
			{
			1, 2, 3, 4, 5, 6, 7, 8,
			9, 10, 11, 12, 13, 14, 15, 16
			};

			byte[] combinedKey = new byte[Key.Length + salt.Length];
			Buffer.BlockCopy(Key, 0, combinedKey, 0, Key.Length);
			Buffer.BlockCopy(salt, 0, combinedKey, Key.Length, salt.Length);

			var symEncryptedData = Convert.FromBase64String(entryText);
			byte[] symUnencryptedData;
			using (var decryptor = Algorithm.CreateDecryptor(combinedKey, iniVetor))
			using (var memoryStream = new MemoryStream())
			using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Write))
			{
				cryptoStream.Write(symEncryptedData, 0, symEncryptedData.Length);
				cryptoStream.FlushFinalBlock();
				symUnencryptedData = memoryStream.ToArray();
			}
			Algorithm.Dispose();
			return Encoding.Default.GetString(symUnencryptedData);
		}
	}
}
