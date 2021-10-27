using System;
using System.ComponentModel.DataAnnotations;

namespace ProductManagement.API.DTOs.Input
{
    public class UpdateEndereco
    {
        [Required(ErrorMessage = "O {0} do fornecedor é requerido!")]
        public Guid FornecedorId { get; set; }
        [Required(ErrorMessage = "O {0} do fornecedor é requerido!")]
        public string Logradouro { get; set; }
        [Required(ErrorMessage = "O {0} do fornecedor é requerido!")]
        public int Numero { get; set; }
        [Required(ErrorMessage = "O {0} do fornecedor é requerido!")]
        public string Complemento { get; set; }
        [Required(ErrorMessage = "O {0} do fornecedor é requerido!")]
        public string Cep { get; set; }
        [Required(ErrorMessage = "O {0} do fornecedor é requerido!")]
        public string Bairro { get; set; }
        [Required(ErrorMessage = "O {0} do fornecedor é requerido!")]
        public string Cidade { get; set; }
        [Required(ErrorMessage = "O {0} do fornecedor é requerido!")]
        public string Estado { get; set; }
    }
}
