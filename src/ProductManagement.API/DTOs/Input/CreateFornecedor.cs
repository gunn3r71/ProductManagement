using ProductManagement.Business.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace ProductManagement.API.DTOs.Input
{
    public class CreateFornecedor
    {
        [Required(ErrorMessage ="O {0} do fornecedor é requerido!")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O {0} do fornecedor é requerido!")]
        public string Documento { get; set; }
        [Required(ErrorMessage = "O {0} do fornecedor é requerido!")]
        [Display(Name ="Tipo")]
        public TipoFornecedor TipoFornecedor { get; set; }
        public CreateEndereco Endereco { get; set; }

        public class CreateEndereco
        {
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
}
