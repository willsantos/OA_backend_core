using Microsoft.AspNetCore.Http;
using OA_Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_Core.Domain.Interfaces.Service
{
	public interface IImagemService
	{
		Task<string> SaveImageAsync(IFormFile file, TipoImagem tipoImagem);

	}
}
