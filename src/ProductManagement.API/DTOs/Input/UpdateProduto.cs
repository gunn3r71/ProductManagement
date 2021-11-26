using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace ProductManagement.API.DTOs.Input
{
    public class UpdateProduto
    {
        [Required(ErrorMessage = "O {0} de produto é requerido!")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O {0} de produto é requerido!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A {0} de produto é requerido!")]
        public string Descricao { get; set; }

        public string Imagem { get; set; }

        public IFormFile ImagemUrl { get; set; }

        [Required(ErrorMessage = "O {0} de produto é requerido!")]
        public decimal Valor { get; set; }

        [Required(ErrorMessage = "O {0} de produto é requerido!")]
        public Guid FornecedorId { get; set; }
    }
}
