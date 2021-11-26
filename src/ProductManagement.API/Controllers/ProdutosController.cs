using AutoMapper;
using Microsoft.AspNetCore.Http;
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

        [RequestSizeLimit(25000)]
        [HttpPost]
        public async Task<IActionResult> Add(CreateProduto produtoModel)
        {
            if (!ModelState.IsValid) return CustomErrorResponse(ModelState);

            var imgName = Guid.NewGuid() + produtoModel.ImagemUrl.FileName; 

            if (!await UploadArquivo(produtoModel.ImagemUrl, imgName)) return CustomErrorResponse();

            var produto = _mapper.Map<Produto>(produtoModel);

            produto.Imagem = imgName;

            await _produtoService.Adicionar(produto);

            return CreatedAtRoute("",new { Id = produto.Id}, produto);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, UpdateProduto updateProduto)
        {
            if (id != updateProduto.Id) return CustomErrorResponse("Os Id's informados são ");
            
            var produtoAtualizacao = await GetProduto(id);
            updateProduto.Imagem = produtoAtualizacao.Imagem;

            if (!ModelState.IsValid) return CustomErrorResponse(ModelState);

            if (updateProduto.ImagemUrl is not null)
            {
                var imgName = Guid.NewGuid() + updateProduto.ImagemUrl.FileName;

                if (!await UploadArquivo(updateProduto.ImagemUrl, imgName)) return CustomErrorResponse();

                produtoAtualizacao.Imagem = imgName;
            }

            produtoAtualizacao.Nome = updateProduto.Nome;
            produtoAtualizacao.Descricao = updateProduto.Descricao;
            produtoAtualizacao.Valor = updateProduto.Valor;
            produtoAtualizacao.Ativo = produtoAtualizacao.Ativo;

            await _produtoService.Atualizar(_mapper.Map<Produto>(produtoAtualizacao));

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> RemoveById(Guid id)
        {
            var produto = await GetProduto(id);

            if (produto is null) return NotFound();

            await _produtoService.Remover(id);

            return NoContent();
        }

        private async Task<bool> UploadArquivo(IFormFile arquivo, string nomeArquivo)
        {
            if (arquivo is null || arquivo is { Length: 0})
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

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await arquivo.CopyToAsync(stream);
            }

            return true;
        }

        private async Task<ProdutoFornecedorOutput> GetProduto(Guid id)
        {
            var produto = await _produtoRepository.ObterProdutoFornecedor(id);

            return _mapper.Map<ProdutoFornecedorOutput>(produto);
        }
    }
}
