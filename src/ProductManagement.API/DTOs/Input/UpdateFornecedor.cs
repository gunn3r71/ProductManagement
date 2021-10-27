using ProductManagement.Business.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace ProductManagement.API.DTOs.Input
{
    public partial class UpdateFornecedor
    {
        [Required(ErrorMessage = "O {0} do fornecedor é requerido!")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "O {0} do fornecedor é requerido!")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O {0} do fornecedor é requerido!")]
        public string Documento { get; set; }
        [Required(ErrorMessage = "O {0} do fornecedor é requerido!")]
        [Display(Name = "Tipo")]
        public TipoFornecedor TipoFornecedor { get; set; }
    }
}
