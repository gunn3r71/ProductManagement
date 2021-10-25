using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.API.DTOs.Output;
using ProductManagement.Business.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductManagement.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class FornecedoresController : BaseController
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IFornecedorService _fornecedorService;
        private readonly IMapper _mapper;

        public FornecedoresController(IFornecedorService fornecedorService, IFornecedorRepository fornecedorRepository, IMapper mapper)
        {
            _fornecedorService = fornecedorService;
            _fornecedorRepository = fornecedorRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var fornecedores = await _fornecedorRepository.ObterTodos();

            return Ok(_mapper.Map<IEnumerable<FornecedorOutput>>(fornecedores));
        }
    }
}
