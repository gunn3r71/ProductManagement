using AutoMapper;
using KissLog;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.API.DTOs.Input;
using ProductManagement.API.DTOs.Output;
using ProductManagement.Business.Interfaces;
using ProductManagement.Business.Models;
using System;
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
        private readonly ILogger _logger;

        public FornecedoresController(IFornecedorService fornecedorService,
                                      IFornecedorRepository fornecedorRepository,
                                      IMapper mapper,
                                      INotificador notificador,
                                      ILogger logger)
            : base(notificador)
        {
            _fornecedorService = fornecedorService;
            _fornecedorRepository = fornecedorRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var fornecedores = await _fornecedorRepository.ObterTodos();

                return Ok(_mapper.Map<IEnumerable<FornecedorOutput>>(fornecedores));
            }
            catch (Exception ex)
            {
                _logger.Error($"Source: {ex.Source}" +
                                 $"StackTrace: {ex.StackTrace} \n" +
                                 $"Error Message: {ex.Message} \n" +
                                 $"Inner Exception: {ex.InnerException}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id:guid}", Name = nameof(GetById))]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            try
            {
                var fornecedor = await GetProviderWithAddressAndProductsAsync(id);
                if (fornecedor is null) return NotFound();

                return Ok(_mapper.Map<FornecedorEnderecoProdutosOutput>(fornecedor));
            }
            catch (Exception ex)
            {
                _logger.Error($"Source: {ex.Source}" +
                    $"StackTrace: {ex.StackTrace} \n" +
                    $"Error Message: {ex.Message} \n" +
                    $"Inner Exception: {ex.InnerException}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateFornecedor fornecedorModel)
        {
            try
            {
                if (!ModelState.IsValid) return CustomErrorResponse(ModelState);

                var fornecedor = _mapper.Map<Fornecedor>(fornecedorModel);

                var result = await _fornecedorService.Adicionar(fornecedor);

                if (!result) return CustomErrorResponse(ModelState);

                return CreatedAtRoute(nameof(GetById), new { Id = fornecedor.Id }, fornecedorModel);
            }
            catch (Exception ex)
            {
                _logger.Error($"Source: {ex.Source}" +
                    $"StackTrace: {ex.StackTrace} \n" +
                    $"Error Message: {ex.Message} \n" +
                    $"Inner Exception: {ex.InnerException}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateFornecedor fornecedorModel)
        {
            try
            {
                if (id != fornecedorModel.Id) return CustomErrorResponse("Os id's fornecidos são diferentes.");

                var fornecedor = _mapper.Map<Fornecedor>(fornecedorModel);
                var result = await _fornecedorService.Atualizar(fornecedor);

                if (!result) return CustomErrorResponse(ModelState);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.Error($"Source: {ex.Source}" +
                    $"StackTrace: {ex.StackTrace} \n" +
                    $"Error Message: {ex.Message} \n" +
                    $"Inner Exception: {ex.InnerException}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("{fornecedorId:guid}/endereco")]
        public async Task<IActionResult> UpdateAddress(Guid fornecedorId, [FromBody] UpdateEndereco enderecoModel)
        {
            try
            {
                if (fornecedorId != enderecoModel.FornecedorId) return CustomErrorResponse("Os id's fornecidos são diferentes.");

                var endereco = _mapper.Map<Endereco>(enderecoModel);
                var result = await _fornecedorService.AtualizarEndereco(endereco);

                if (!result) return CustomErrorResponse(ModelState);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.Error($"Source: {ex.Source}" +
                    $"StackTrace: {ex.StackTrace} \n" +
                    $"Error Message: {ex.Message} \n" +
                    $"Inner Exception: {ex.InnerException}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

        }


        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Remove(Guid id)
        {
            try
            {
                var result = await _fornecedorService.Remover(id);

                if (!result) return CustomErrorResponse(ModelState);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.Error($"Source: {ex.Source}" +
                    $"StackTrace: {ex.StackTrace} \n" +
                    $"Error Message: {ex.Message} \n" +
                    $"Inner Exception: {ex.InnerException}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

        }

        private async Task<FornecedorEnderecoProdutosOutput> GetProviderWithAddressAndProductsAsync(Guid providerId)
        {
            var fornecedor = _mapper.Map<FornecedorEnderecoProdutosOutput>
                             (await _fornecedorRepository.ObterFornecedorProdutosEndereco(providerId));

            return fornecedor;
        }
    }
}
