using FluentValidation;
using ProductManagement.Business.Models;

namespace ProductManagement.Business.Validations
{
    public class ProdutoValidation : AbstractValidator<Produto>
    {
        public ProdutoValidation()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido.")
                .Length(2, 70)
                .WithMessage("O campo {PropertyName} deve ter tamanho entre {MinLength} e {MaxLength} caracteres.");

            RuleFor(x => x.Descricao)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido.")
                .Length(2, 255)
                .WithMessage("O campo {PropertyName} deve ter tamanho entre {MinLength} e {MaxLength} caracteres.");

            RuleFor(x => x.Valor)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido.")
                .GreaterThan(0).WithMessage("O campo {PropertyName} precisa ser maior que {ComparisonValue}.");
        }
    }
}