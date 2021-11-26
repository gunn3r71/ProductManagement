using AutoMapper;
using ProductManagement.API.DTOs.Input;
using ProductManagement.API.DTOs.Output;
using ProductManagement.Business.Models;
using static ProductManagement.API.DTOs.Input.CreateFornecedor;

namespace ProductManagement.API.Configuration
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Fornecedor, FornecedorOutput>();
            CreateMap<Fornecedor, FornecedorEnderecoOutput>();
            CreateMap<Fornecedor,FornecedorEnderecoProdutosOutput>();
            CreateMap<Endereco, EnderecoOutput>();
            CreateMap<Produto, ProdutoOutput>();
            CreateMap<Produto, ProdutoFornecedorOutput>();

            CreateMap<CreateFornecedor, Fornecedor>();
            CreateMap<CreateEndereco, Endereco>();
            CreateMap<CreateProduto, Produto>();

            CreateMap<UpdateProduto, Produto>();
            CreateMap<UpdateFornecedor, Fornecedor>();
            CreateMap<UpdateEndereco, Endereco>();
        }
    }
}
