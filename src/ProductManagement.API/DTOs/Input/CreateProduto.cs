using System;
using System.ComponentModel.DataAnnotations;

namespace ProductManagement.API.DTOs.Input
{
    public class CreateProduto
    {
        [Required(ErrorMessage = "O {0} de produto é requerido!")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "A {0} de produto é requerido!")]
        public string Descricao { get; set; }
        public string Imagem { get; set; }
        public string ImagemUrl { get; set; }
        [Required(ErrorMessage = "O {0} de produto é requerido!")]
        public decimal Valor { get; set; }

        [Required(ErrorMessage = "O {0} de produto é requerido!")]
        public Guid FornecedorId { get; set; }
    }
}
