using ProductManagement.Business.Models;
using System;
using System.Threading.Tasks;

namespace ProductManagement.Business.Interfaces
{
    public interface IEnderecoRepository : IRepository<Endereco>
    {
        Task<Endereco> ObterEnderecoFornecedor(Guid fornecedorId);
    }
}