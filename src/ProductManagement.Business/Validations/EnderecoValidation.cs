using FluentValidation;
using ProductManagement.Business.Models;

namespace ProductManagement.Business.Validations
{
    public class EnderecoValidation : AbstractValidator<Endereco>
    {
        public EnderecoValidation()
        {
            RuleFor(x => x.Logradouro)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido.")
                .Length(4, 100)
                .WithMessage("O campo {PropertyName} deve ter tamanho entre {MinLength} e {MaxLength} caracteres.");
            RuleFor(x => x.Cep)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido.")
                .Length(8, 8)
                .WithMessage("O campo {PropertyName} deve ter tamanho de {MaxLength} caracteres.");
            RuleFor(x => x.Bairro)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido.")
                .Length(3, 100)
                .WithMessage("O campo {PropertyName} deve ter tamanho entre {MinLength} e {MaxLength} caracteres.");
            RuleFor(x => x.Numero)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido.");
            RuleFor(x => x.Complemento)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido.")
                .Length(2, 100)
                .WithMessage("O campo {PropertyName} deve ter tamanho entre {MinLength} e {MaxLength} caracteres.");
            RuleFor(x => x.Cidade)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido.")
                .Length(3, 100)
                .WithMessage("O campo {PropertyName} deve ter tamanho entre {MinLength} e {MaxLength} caracteres.");
            RuleFor(x => x.Estado)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido.")
                .Length(2, 2)
                .WithMessage("O campo {PropertyName} deve ter tamanho de {MaxLength} caracteres.");
        }
    }
}