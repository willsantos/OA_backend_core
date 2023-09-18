using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using OA_Core.Domain.Interfaces.Service;
using static System.Net.Mime.MediaTypeNames;
using System.IO;

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

		public async Task<string> SaveImageAsync(IFormFile file)
		{
			if (file == null || file.Length == 0)
			{
				throw new ArgumentException("Arquivo inválido");
			}

			// Certifica de que a pasta "images" existe, criando ela se necessário.
			Directory.CreateDirectory(_imageFolderPath);

			// Formata nome do arquivo
			string filename = file.FileName.Split('.')[0];

			// Gere um nome de arquivo único para evitar conflitos.
			string uniqueFileName = filename + "_" + Guid.NewGuid().ToString() + ".jpg";

			// Combine o caminho completo do arquivo.
			string filePath = Path.Combine(_imageFolderPath, uniqueFileName);

			// Salve o arquivo no diretório de imagens.
			using (var fileStream = new FileStream(filePath, FileMode.Create))
			{
				await file.CopyToAsync(fileStream);
			}
		
			// Retorne o caminho completo do arquivo para uso futuro.
			return Path.Combine("images", uniqueFileName);
		}
	}
}
