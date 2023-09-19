using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using OA_Core.Domain.Interfaces.Service;
using static System.Net.Mime.MediaTypeNames;
using System.IO;
using OA_Core.Domain.Enums;
using OA_Core.Domain.Interfaces.Notifications;
using OA_Core.Domain.Exceptions;

namespace OA_Core.Service
{
	public class ImagemService : IImagemService
	{
		private readonly IHostingEnvironment _environment;
		private readonly string _imageFolderPath;

		public ImagemService(IHostingEnvironment environment)
		{
			_environment = environment;
			_imageFolderPath = Path.Combine(_environment.ContentRootPath, "images");
		}

		public async Task<string> SaveImageAsync(IFormFile file, TipoImagem TipoImagem)
		{
			if (file == null || file.Length == 0)
			{
				throw new InformacaoException(StatusException.FormatoIncorreto, $"Arquivo não encontrado");
			}

			if (!file.ContentType.StartsWith("image"))
			{
				throw new InformacaoException(StatusException.FormatoIncorreto, $"Formato de arquivo inválido");
			}

			string caminhoImagem = TipoImagem.ToString().ToLower(); // Use o nome da enumeração em minúsculas

			// Certifica de que a pasta "images" existe, criando ela se necessário.
			Directory.CreateDirectory(_imageFolderPath);

			//Cerficica que a subpasta existe, criando se seja necessário.
			string subfolderPath = Path.Combine(_imageFolderPath, caminhoImagem);
			Directory.CreateDirectory(subfolderPath);

			// Formata nome do arquivo
			string filename = Path.GetFileNameWithoutExtension(file.FileName);

			// Gere um nome de arquivo único para evitar conflitos.
			string uniqueFileName = filename + "_" + Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

			// Combine o caminho completo do arquivo.
			string filePath = Path.Combine(subfolderPath, uniqueFileName);

			// Salve o arquivo no diretório de imagens.
			using (var fileStream = new FileStream(filePath, FileMode.Create))
			{
				await file.CopyToAsync(fileStream);
			}

			// Retorne o caminho completo do arquivo para uso futuro.
			return Path.Combine("images", caminhoImagem, uniqueFileName);
		}
	}
}
