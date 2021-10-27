using System;
using System.Linq;
using System.Threading.Tasks;
using ProductManagement.Business.Interfaces;
using ProductManagement.Business.Models;
using ProductManagement.Business.Validations;

namespace ProductManagement.Business.Services
{
    public class FornecedorService : BaseService, IFornecedorService
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IEnderecoRepository _enderecoRepository;

        public FornecedorService(IFornecedorRepository fornecedorRepository, IEnderecoRepository enderecoRepository, INotificador notificador)
            : base(notificador)
        {
            _fornecedorRepository = fornecedorRepository;
            _enderecoRepository = enderecoRepository;
        }

        public async Task<bool> Adicionar(Fornecedor fornecedor)
        {
            if (!ExecutarValidacao(new FornecedorValidation(), fornecedor)
                && !ExecutarValidacao(new EnderecoValidation(), fornecedor.Endereco)) return false;

            if (_fornecedorRepository.Buscar(x => x.Documento == fornecedor.Documento).Result.Any())
            {
                Notify("Já existe um fornecedor com o documento informado.");
                return false;
            }

            await _fornecedorRepository.Adicionar(fornecedor);

            return true;
        }

        public async Task<bool> Atualizar(Fornecedor fornecedor)
        {
            if (!ExecutarValidacao(new FornecedorValidation(), fornecedor)) return false;

            if (_fornecedorRepository.Buscar(x => x.Documento == fornecedor.Documento && x.Id != fornecedor.Id).Result.Any())
            {
                Notify("Já existe um fornecedor com o documento informado.");
                return false;
            }

            var fornecedorUpdate = await _fornecedorRepository.ObterPorId(fornecedor.Id);

            fornecedorUpdate.Nome = fornecedor.Nome;
            fornecedorUpdate.Documento = fornecedor.Documento;
            fornecedorUpdate.TipoFornecedor = fornecedor.TipoFornecedor;

            await _fornecedorRepository.Atualizar(fornecedorUpdate);
            return true;
        }

        public async Task<bool> Remover(Guid id)
        {
            if (_fornecedorRepository.ObterFornecedorProdutosEndereco(id).Result.Produtos.Any())
            {
                Notify("O fornecedor possui produtos cadastrados.");
                return false;
            }

            await _fornecedorRepository.Remover(id);
            return true;
        }

        public async Task<bool> AtualizarEndereco(Endereco endereco)
        {
            if (!ExecutarValidacao(new EnderecoValidation(), endereco)) return false;

            var fornecedor = await _fornecedorRepository.ObterFornecedorEndereco(endereco.FornecedorId);

            fornecedor.Endereco.Logradouro = endereco.Logradouro;
            fornecedor.Endereco.Numero = endereco.Numero;
            fornecedor.Endereco.Complemento = endereco.Complemento;
            fornecedor.Endereco.Cep = endereco.Cep;
            fornecedor.Endereco.Bairro = endereco.Bairro;
            fornecedor.Endereco.Cidade = endereco.Cidade;
            fornecedor.Endereco.Estado = endereco.Estado;

            await _enderecoRepository.Atualizar(fornecedor.Endereco);

            return true;
        }

        public void Dispose()
        {
            _fornecedorRepository?.Dispose();
            _enderecoRepository?.Dispose();
        }
    }
}