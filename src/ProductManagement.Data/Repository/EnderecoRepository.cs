using Microsoft.EntityFrameworkCore;
using ProductManagement.Business.Interfaces;
using ProductManagement.Business.Models;
using ProductManagement.Data.Context;
using System;
using System.Threading.Tasks;

namespace ProductManagement.Data.Repository
{
    public class EnderecoRepository : Repository<Endereco>, IEnderecoRepository
    {
        public EnderecoRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Endereco> ObterEnderecoFornecedor(Guid fornecedorId)
        {
            return await _context.Enderecos.AsNoTracking().FirstOrDefaultAsync(e => e.FornecedorId == fornecedorId);
        }
    }
}