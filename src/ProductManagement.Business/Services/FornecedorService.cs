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

            await _fornecedorRepository.Atualizar(fornecedor);
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

        public async Task AtualizarEndereco(Endereco endereco)
        {
            if (!ExecutarValidacao(new EnderecoValidation(), endereco)) return;

            await _enderecoRepository.Atualizar(endereco);
        }

        public void Dispose()
        {
            _fornecedorRepository?.Dispose();
            _enderecoRepository?.Dispose();
        }
    }
}