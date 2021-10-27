using System;
using System.Threading.Tasks;
using ProductManagement.Business.Models;

namespace ProductManagement.Business.Interfaces
{
    public interface IFornecedorService : IDisposable
    {
        Task<bool> Adicionar(Fornecedor fornecedor);
        Task<bool> Atualizar(Fornecedor fornecedor);
        Task<bool> Remover(Guid id);

        Task<bool> AtualizarEndereco(Endereco endereco);
    }
}