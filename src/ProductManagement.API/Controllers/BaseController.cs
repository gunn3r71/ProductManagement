using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ProductManagement.API.DTOs.Output;
using ProductManagement.API.Filters;
using ProductManagement.Business.Interfaces;
using ProductManagement.Business.Notifications;
using System;
using System.Linq;

namespace ProductManagement.API.Controllers
{
    [Authorize]
    [ApiController]
    [ServiceFilter(typeof(ApiLogFilter))]
    public abstract class BaseController : ControllerBase
    {
        private readonly INotificador _notificador;
        protected readonly IUser AppUser;
        protected Guid UsuarioId { get; set; }
        protected bool UsuarioAutenticado { get; set; }

        protected BaseController(INotificador notificador, 
                                 IUser appUser)
        {
            _notificador = notificador;
            AppUser = appUser;

            if (appUser.IsAuthenticated())
            {
                UsuarioId = appUser.GetUserId();
                UsuarioAutenticado = appUser.IsAuthenticated();
            }
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
            return BadRequest(new CustomResponseOutput
            {
                Success = false,
                Message = "Ocorreram um ou mais erros em sua solicitação",
                Data = new ErrorOutput
                {
                    Errors = _notificador.ObterNotificacoes()
                }
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
