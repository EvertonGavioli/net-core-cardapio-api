using CD.Domain.Interfaces;
using CD.Domain.Notificacoes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CD.API.Controllers
{
    [ApiController]
    [Produces("application/json")]
    public abstract class MainController : Controller
    {
        private readonly INotificador _notificador;
        public readonly IUser _aspNetUser;

        protected Guid UsuarioId { get; set; }
        protected bool UsuarioAutenticado { get; set; }

        protected MainController(INotificador notificador, IUser aspNetUser)
        {
            _notificador = notificador;
            _aspNetUser = aspNetUser;

            if (_aspNetUser.IsAuthenticated())
            {
                UsuarioId = _aspNetUser.GetUserId();
                UsuarioAutenticado = true;
            }
        }

        protected ActionResult CustomResponse(object result = null)
        {
            if (OperacaoValida())
            {
                return Ok(result);
            }

            var resultErros = new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                { "Mensagens", _notificador.ObterNotificacoes().Select(n => n.Mensagem).ToArray() }
            });

            return BadRequest(resultErros);
        }

        protected ActionResult CustomResponse(string methodNameCreated, object result = null)
        {
            if (OperacaoValida())
            {
                return CreatedAtAction(methodNameCreated, result);
            }

            var resultErros = new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                { "Mensagens", _notificador.ObterNotificacoes().Select(n => n.Mensagem).ToArray() }
            });

            return BadRequest(resultErros);
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(e => e.Errors);
            foreach (var erro in erros)
            {
                AdicionarErro(erro.ErrorMessage);
            }

            return CustomResponse();
        }

        protected bool OperacaoValida()
        {
            return !_notificador.PossuiNotificacao();
        }

        protected void AdicionarErro(string mensagem)
        {
            _notificador.Handle(new Notificacao(mensagem));
        }
    }
}
