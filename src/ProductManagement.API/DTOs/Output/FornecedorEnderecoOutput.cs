using System;

namespace ProductManagement.API.DTOs.Output
{
    public class FornecedorEnderecoOutput : FornecedorOutput
    {
        public EnderecoOutput Endereco { get; set; }
    }
}
