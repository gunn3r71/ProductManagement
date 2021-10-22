using System.Collections.Generic;
using ProductManagement.Business.Notifications;

namespace ProductManagement.Business.Interfaces
{
    public interface INotificador
    {
        bool TemNotificacoes();
        List<Notificacao> ObterNotificacoes();
        void Handle(Notificacao notificacao);
    }
}