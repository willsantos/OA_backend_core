using FluentValidation.Results;
using OA_Core.Domain.Notifications;

namespace OA_Core.Domain.Interfaces.Notifications
{
    public interface INotificador
    {
        bool TemNotificacao();
        List<Notificacao> ObterNotificacoes();
        void Handle(Notificacao notificacao);
        void Handle(ValidationResult validationResult);
    }
}
