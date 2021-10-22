using System;
using System.Threading.Tasks;
using ProductManagement.Business.Models;

namespace ProductManagement.Business.Interfaces
{
    public interface IProdutoService : IDisposable
    {
        Task Adicionar(Produto produto);
        Task Atualizar(Produto produto);
        Task Remover(Guid id);
    }
}