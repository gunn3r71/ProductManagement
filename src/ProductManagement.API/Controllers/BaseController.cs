using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ProductManagement.API.DTOs.Output;
using ProductManagement.Business.Interfaces;
using ProductManagement.Business.Notifications;
using System.Linq;
using ProductManagement.API.Filters;

namespace ProductManagement.API.Controllers
{
    [ApiController]
    [ServiceFilter(typeof(ApiLogFilter))]
    public abstract class BaseController : ControllerBase
    {

        private readonly INotificador _notificador;

        protected BaseController(INotificador notificador)
        {
            _notificador = notificador;
        }

        protected ActionResult CustomErrorResponse(ModelStateDictionary modelState)
        {
            NotifyErrorInvalidModel(modelState);

            return CustomErrorResponse();
        }

        protected ActionResult CustomErrorResponse(string erro)
        {
            ModelState.AddModelError("Errors", erro);

            NotifyErrorInvalidModel(ModelState);

            return CustomErrorResponse();
        }

        protected ActionResult CustomErrorResponse()
        {
            return BadRequest(new ErrorOutput
            {
                Errors = _notificador.ObterNotificacoes()
            });
        }   

        protected void NotifyErrorInvalidModel(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(x => x.Errors);
            foreach (var erro in erros)
            {
                var errorMessage = erro.Exception is null ? erro.ErrorMessage : erro.Exception.Message;
                NotifyError(errorMessage);
            }
        }

        protected void NotifyError(string message)
        {
            _notificador.Handle(new Notificacao(message));
        }
    }
}
