using Microsoft.AspNetCore.Mvc.Filters;
using OA_Core.Domain.Contracts.Response;
using OA_Core.Domain.Enums;
using OA_Core.Domain.Interfaces.Notifications;
using System.Net;
using System.Text.Json;

namespace OA_Core.Api.Filters
{
    public class NotificatonFilter : IAsyncResultFilter
    {
        private readonly INotificador _notificador;

        public NotificatonFilter(INotificador notificador)
        {
            _notificador = notificador;
        }
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if(_notificador.TemNotificacao())
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.HttpContext.Response.ContentType = "application/json";

                var mensagens = new List<string>();

                foreach(var mensagem in _notificador.ObterNotificacoes())
                {
                    mensagens.Add(mensagem.Mensagem.ToString());
                }

                var informacaoResponse = new InformacaoResponse()
                {
                    Codigo = StatusException.FormatoIncorreto,
                    Mensagens = mensagens
                };

                var notifications = JsonSerializer.Serialize(informacaoResponse);

                await context.HttpContext.Response.WriteAsync(notifications);

                return;
            }

            await next();
        }
    }
}
