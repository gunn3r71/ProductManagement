using FluentValidation;
using ProductManagement.Business.Models;
using ProductManagement.Business.Models.Enums;
using ProductManagement.Business.Validations.Documents;

namespace ProductManagement.Business.Validations
{
    public class FornecedorValidation : AbstractValidator<Fornecedor>
    {
        public FornecedorValidation()
        {
            RuleFor(f => f.Nome)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido.")
                .Length(2, 100)
                .WithMessage("O campo {PropertyName} deve ter tamanho entre {MinLength} e {MaxLength} caracteres.");
            RuleFor(t => t.TipoFornecedor)
                .NotNull().WithMessage("O campo {PropertyName} precisa ser fornecido.");

            When(f => f.TipoFornecedor == TipoFornecedor.PessoaFisica, () =>
            {
                RuleFor(f => f.Documento.Length)
                    .Equal(DocsValidation.TAMANHO_CPF)
                    .WithMessage("O campo documento deve ter tamanho {ComparisonValue} e foi fornecido {PropertyValue}.");
                RuleFor(f => DocsValidation.IsCpf(f.Documento))
                    .Equal(true)
                    .WithMessage("O documento fornecido é inválido.");
            });
            When(f => f.TipoFornecedor == TipoFornecedor.PessoaJuridica, () =>
            {
                RuleFor(f => f.Documento.Length)
                    .Equal(DocsValidation.TAMANHO_CNPJ)
                    .WithMessage("O campo documento deve ter tamanho {ComparisonValue} e foi fornecido {PropertyValue}.");
                RuleFor(f => DocsValidation.IsCnpj(f.Documento))
                    .Equal(true)
                    .WithMessage("O documento fornecido é inválido.");
            });
        }
    }
}