using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.API.Extensions;
using System;
using System.ComponentModel.DataAnnotations;

namespace ProductManagement.API.DTOs.Input
{
    [ModelBinder(typeof(JsonWithFilesFormDataModelBinder), Name = "produto")]
    public class CreateProduto
    {
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
