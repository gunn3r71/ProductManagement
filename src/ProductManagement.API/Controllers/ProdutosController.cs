﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.API.DTOs.Input;
using ProductManagement.API.DTOs.Output;
using ProductManagement.API.Filters;
using ProductManagement.Business.Interfaces;
using ProductManagement.Business.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ProductManagement.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ServiceFilter(typeof(ApiLoggingFilter))]
    public class ProdutosController : BaseController
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IProdutoService _produtoService;
        private readonly IMapper _mapper;

        public ProdutosController(INotificador notificador,
            IMapper mapper,
            IProdutoService produtoService,
            IProdutoRepository produtoRepository)
            : base(notificador)
        {
            _mapper = mapper;
            _produtoService = produtoService;
            _produtoRepository = produtoRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var produtos = await _produtoRepository.ObterProdutosFornecedores();

            return Ok(_mapper.Map<IEnumerable<ProdutoFornecedorOutput>>(produtos));
        }

        [HttpGet("fornecedor/{fornecedorId:guid}")]
        public async Task<IActionResult> GetProdutosByFornecedor(Guid fornecedorId)
        {
            var produtos = await _produtoRepository.ObterProdutosPorFornecedor(fornecedorId);

            return Ok(_mapper.Map<IEnumerable<ProdutoOutput>>(produtos));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var produto = await GetProduto(id);

            if (produto is null) return NotFound();

            return Ok(_mapper.Map<ProdutoFornecedorOutput>(produto));
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateProduto produtoModel)
        {
            if (!ModelState.IsValid) return CustomErrorResponse(ModelState);

            var imgName = Guid.NewGuid() + $"_{produtoModel.Imagem}";

            if (!await UploadArquivo(produtoModel.ImagemUrl, imgName)) return CustomErrorResponse();

            var produto = _mapper.Map<Produto>(produtoModel);

            produto.Imagem = imgName;

            await _produtoService.Adicionar(produto);

            return CreatedAtRoute("",new { Id = produto.Id}, produto);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> RemoveById(Guid id)
        {
            var produto = await GetProduto(id);

            if (produto is null) return NotFound();

            await _produtoService.Remover(id);

            return NoContent();
        }

        private async Task<bool> UploadArquivo(string arquivo, string nomeArquivo)
        {
            var imagemDataByteArray = Convert.FromBase64String(arquivo);

            if (string.IsNullOrWhiteSpace(arquivo) || arquivo.Length is 0)
            {
                NotifyError("Forneça uma imagem para o produto.");
                return false;
            }

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens", nomeArquivo);

            if (System.IO.File.Exists(filePath))
            {
                NotifyError("Já existe um arquivo com esse nome");
                return false;
            }

            await System.IO.File.WriteAllBytesAsync(filePath, imagemDataByteArray);

            return true;
        }

        private async Task<ProdutoFornecedorOutput> GetProduto(Guid id)
        {
            var produto = await _produtoRepository.ObterProdutoFornecedor(id);

            return _mapper.Map<ProdutoFornecedorOutput>(produto);
        }
    }
}