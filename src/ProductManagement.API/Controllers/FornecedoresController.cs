using Microsoft.AspNetCore.Mvc;
using ProductManagement.Business.Interfaces;
using System.Threading.Tasks;

namespace ProductManagement.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class FornecedoresController : BaseController
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IFornecedorService _fornecedorService;

        public FornecedoresController(IFornecedorService fornecedorService, IFornecedorRepository fornecedorRepository)
        {
            _fornecedorService = fornecedorService;
            _fornecedorRepository = fornecedorRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var fornecedores = await _fornecedorRepository.ObterTodos();
            return Ok(fornecedores);
        }
    }
}
