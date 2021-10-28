using Microsoft.AspNetCore.Mvc;
using ProductManagement.Business.Interfaces;

namespace ProductManagement.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class ProdutosController : BaseController
    {
    //    private readonly 
        public ProdutosController(INotificador notificador) : base(notificador)
        {
        }
    }
}
