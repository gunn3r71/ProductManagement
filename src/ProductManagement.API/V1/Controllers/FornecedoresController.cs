using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using KissLog;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.API.Controllers;
using ProductManagement.API.DTOs.Input;
using ProductManagement.API.DTOs.Output;
using ProductManagement.API.Security.Filters;
using ProductManagement.Business.Interfaces;
using ProductManagement.Business.Models;

namespace ProductManagement.API.V1.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class FornecedoresController : BaseController
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IFornecedorService _fornecedorService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public FornecedoresController(INotificador notificador,
            IUser appUser, 
            IFornecedorRepository fornecedorRepository,
            IFornecedorService fornecedorService, 
            IMapper mapper,
            ILogger logger) : base(notificador, appUser)
        {
            _fornecedorRepository = fornecedorRepository;
            _fornecedorService = fornecedorService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var fornecedores = await _fornecedorRepository.ObterTodos();

            return Ok(new CustomResponseOutput
            {
                Success = true,
                Message = "Sucesso",
                Data = _mapper.Map<IEnumerable<FornecedorOutput>>(fornecedores)
            });
        }
        
        [HttpGet("{id:guid}", Name = nameof(GetById))]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var fornecedor = await GetProviderWithAddressAndProductsAsync(id);
            if (fornecedor is null) return NotFound(new CustomResponseOutput
            {
                Success = false,
                Message = "Fornecedor não foi encontrado.",
                Data = null
            });

            return Ok(new CustomResponseOutput
            {
                Success = true,
                Message = "Sucesso",
                Data = _mapper.Map<FornecedorEnderecoProdutosOutput>(fornecedor)
            });
        }

        [ClaimsAuthorize("Fornecedor", "Adicionar")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateFornecedor fornecedorModel)
        {
            if (!ModelState.IsValid) return CustomErrorResponse(ModelState);

            var fornecedor = _mapper.Map<Fornecedor>(fornecedorModel);

            var result = await _fornecedorService.Adicionar(fornecedor);

            if (!result) return CustomErrorResponse(ModelState);

            return CreatedAtRoute(nameof(GetById), new { Id = fornecedor.Id }, new CustomResponseOutput
            {
                Success = true,
                Message = "Fornecedor criado com sucesso",
                Data = null
            });
        }

        [ClaimsAuthorize("Fornecedor", "Atualizar")]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateFornecedor fornecedorModel)
        {
            if (id != fornecedorModel.Id) return CustomErrorResponse("Os id's fornecidos são diferentes.");

            var fornecedor = _mapper.Map<Fornecedor>(fornecedorModel);
            var result = await _fornecedorService.Atualizar(fornecedor);

            if (!result) return CustomErrorResponse(ModelState);

            return NoContent();
        }

        [ClaimsAuthorize("Fornecedor", "Atualizar")]
        [HttpPut("{fornecedorId:guid}/endereco")]
        public async Task<IActionResult> UpdateAddress(Guid fornecedorId, [FromBody] UpdateEndereco enderecoModel)
        {
            if (fornecedorId != enderecoModel.FornecedorId) return CustomErrorResponse("Os id's fornecidos são diferentes.");

            var endereco = _mapper.Map<Endereco>(enderecoModel);
            var result = await _fornecedorService.AtualizarEndereco(endereco);

            if (!result) return CustomErrorResponse(ModelState);

            return NoContent();
        }

        [ClaimsAuthorize("Fornecedor", "Remover")]
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
