using System;

namespace ProductManagement.API.DTOs.Output
{
    public class FornecedorOutput
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Documento { get; set; }
        public string TipoFornecedor { get; set; }
        public bool Ativo { get; set; }
    }
}
