using System;
using System.Collections.Generic;

namespace ProductManagement.API.DTOs.Output
{
    public class FornecedorEnderecoProdutosOutput : FornecedorEnderecoOutput
    {
        public IEnumerable<ProdutoOutput> Produtos { get; set; }
    }
}
