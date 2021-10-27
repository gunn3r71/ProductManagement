using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.API.DTOs.Input;
using ProductManagement.API.DTOs.Output;
using ProductManagement.Business.Interfaces;
using ProductManagement.Business.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace ProductManagement.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class FornecedoresController : BaseController
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IFornecedorService _fornecedorService;
        private readonly IMapper _mapper;

        public FornecedoresController(IFornecedorService fornecedorService,
                                      IFornecedorRepository fornecedorRepository,
                                      IMapper mapper,
                                      INotificador notificador)
            : base(notificador)
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

        [HttpGet("{id:guid}", Name = nameof(GetById))]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var fornecedor = await GetProviderWithAddressAndProductsAsync(id);
            if (fornecedor is null) return NotFound();

            return Ok(_mapper.Map<FornecedorEnderecoProdutosOutput>(fornecedor));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateFornecedor fornecedorModel)
        {
            if (!ModelState.IsValid) return CustomErrorResponse(ModelState);

            var fornecedor = _mapper.Map<Fornecedor>(fornecedorModel);

            var result = await _fornecedorService.Adicionar(fornecedor);

            if (!result) return CustomErrorResponse(ModelState);

            return CreatedAtRoute(nameof(GetById), new { Id = fornecedor.Id }, fornecedorModel);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateFornecedor fornecedorModel)
        {
            if (id != fornecedorModel.Id) return CustomErrorResponse("Os id's fornecidos são diferentes.");

            var fornecedor = _mapper.Map<Fornecedor>(fornecedorModel);
            var result = await _fornecedorService.Atualizar(fornecedor);

            if (!result) return CustomErrorResponse(ModelState);

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Remove(Guid id)
        {
            var result = await _fornecedorService.Remover(id);

            if (!result) return CustomErrorResponse(ModelState);

            return NoContent();
        }

        private async Task<FornecedorEnderecoProdutosOutput> GetProviderWithAddressAndProductsAsync(Guid providerId)
        {
            var fornecedor = _mapper.Map<FornecedorEnderecoProdutosOutput>
                             (await _fornecedorRepository.ObterFornecedorProdutosEndereco(providerId));

            return fornecedor;
        }
    }
}
