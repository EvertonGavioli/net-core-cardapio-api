using CD.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace CD.Domain.Notificacoes
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

        public List<Notificacao> ObterNotificacoes()
        {
            return _notificacoes;
        }

        public bool PossuiNotificacao()
        {
            return _notificacoes.Any();
        }
    }
}
