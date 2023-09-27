using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OA_Core.Domain.Contracts.Response;
using OA_Core.Domain.Enums;
using OA_Core.Domain.Exceptions;
using OA_Core.Domain.Utils;

namespace OA_Core.Api.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public override Task OnExceptionAsync(ExceptionContext context)
        {
            var response = new InformacaoResponse();
			var environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            if (context.Exception is InformacaoException)
            {
                InformacaoException informacaoException = (InformacaoException)context.Exception;
                response.Codigo = informacaoException.Codigo;
                response.Mensagens = informacaoException.Mensagens;

				if (environment != Environments.Production)
					response.MensagemDebug = context.Exception.Message;
				
			} 
            else
            {
                response.Codigo = StatusException.Erro;
                response.Mensagens = new List<string> { "Erro inesperado " };
				if (environment != Environments.Production)
					response.MensagemDebug = context.Exception.Message;
			}

            context.Result = new ObjectResult(response)
            {
                StatusCode = response.Codigo.GetStatusCode()
            };

            OnException(context);
            return Task.CompletedTask;
        }
    }
}
