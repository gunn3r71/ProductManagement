using System.ComponentModel.DataAnnotations;

namespace ProductManagement.API.DTOs.Input
{
    public class RegisterUser
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "O campo {0} está em um formato inválido.")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "As senhas não conferem.")]
        public string ConfirmPassword { get; set; }
    }
}