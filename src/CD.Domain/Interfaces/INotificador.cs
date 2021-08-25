using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CD.Domain.Notificacoes;

namespace CD.Domain.Interfaces
{
    public interface INotificador
    {
        bool PossuiNotificacao();
        List<Notificacao> ObterNotificacoes();
        void Handle(Notificacao notificacao);
    }
}
