using FluentValidation.Results;
using OA_Core.Domain.Interfaces.Notifications;

namespace OA_Core.Domain.Notifications
{
    public class Notificador : INotificador
    {
        private List<Notificacao> _notificacoes;

        public Notificador()
        {
            _notificacoes = new List<Notificacao>();
        }

        public void Handle(Notificacao notificacao)
        {
            _notificacoes.Add(notificacao);
        }

        public void Handle(ValidationResult validationResult)
        {
            foreach(var erro in validationResult.Errors)
            {
                var notificacao = new Notificacao(erro.ErrorMessage);
                _notificacoes.Add(notificacao);
            }
        }

        public List<Notificacao> ObterNotificacoes()
        {
            return _notificacoes;
        }

        public bool TemNotificacao()
        {
            return _notificacoes.Any();
        }
    }
}
