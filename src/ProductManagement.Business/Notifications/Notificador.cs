using System.Collections.Generic;
using System.Linq;
using ProductManagement.Business.Interfaces;

namespace ProductManagement.Business.Notifications
{
    public class Notificador : INotificador
    {
        public Notificador()
        {
            _notificacoes = new List<Notificacao>();
        }

        private List<Notificacao> _notificacoes;

        public bool TemNotificacoes()
        {
            return _notificacoes.Any();
        }

        public List<Notificacao> ObterNotificacoes()
        {
            return _notificacoes;
        }

        public void Handle(Notificacao notificacao)
        {
            _notificacoes.Add(notificacao);
        }
    }
}