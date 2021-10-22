using Microsoft.EntityFrameworkCore;
using ProductManagement.Business.Interfaces;
using ProductManagement.Business.Models;
using ProductManagement.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagement.Data.Repository
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Produto>> ObterProdutosPorFornecedor(Guid fornecedorId)
        {
            return await _context.Produtos.AsNoTracking().Where(p => p.FornecedorId == fornecedorId).OrderBy(p => p.Nome).ToListAsync();
        }

        public async Task<Produto> ObterProdutoFornecedor(Guid id)
        {
            return await _context.Produtos.AsNoTracking().Include(f => f.Fornecedor).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Produto>> ObterProdutosFornecedores()
        {
            return await _context.Produtos.AsNoTracking().Include(f => f.Fornecedor).OrderBy(p => p.Nome).ToListAsync();
        }
    }
}