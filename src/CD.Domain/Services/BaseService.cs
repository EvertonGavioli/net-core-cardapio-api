using CD.Domain.Interfaces;
using CD.Domain.Models;
using CD.Domain.Notificacoes;
using FluentValidation;
using FluentValidation.Results;
using System;

namespace CD.Domain.Services
{
    public abstract class BaseService
    {
        private readonly INotificador _notificador;
        public readonly IUser _aspNetUser;

        protected Guid UsuarioId { get; set; }
        protected bool UsuarioAutenticado { get; set; }

        protected BaseService(INotificador notificador, IUser aspNetUser)
        {
            _notificador = notificador;
            _aspNetUser = aspNetUser;

            if (_aspNetUser.IsAuthenticated())
            {
                UsuarioId = _aspNetUser.GetUserId();
                UsuarioAutenticado = true;
            }
        }

        protected void Notificar(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Notificar(error.ErrorMessage);
            }
        }

        protected void Notificar(string mensagem)
        {
            _notificador.Handle(new Notificacao(mensagem));
        }

        protected bool ExecutarValidacao<TV, TE>(TV validacao, TE entidade) where TV : AbstractValidator<TE> where TE : Entity
        {
            var validator = validacao.Validate(entidade);

            if (validator.IsValid) return true;

            Notificar(validator);

            return false;
        }
    }
}
