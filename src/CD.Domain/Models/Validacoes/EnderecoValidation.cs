using FluentValidation;

namespace CD.Domain.Models.Validacoes
{
    public class EnderecoValidation : AbstractValidator<Endereco>
    {
        public EnderecoValidation()
        {
            RuleFor(c => c.CEP)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório.")
                .Length(8, 20).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres.");

            RuleFor(c => c.Rua)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório.")
                .Length(1, 200).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres.");

            RuleFor(c => c.Numero)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório.")
                .Length(1, 20).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres.");

            RuleFor(c => c.Bairro)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório.")
                .Length(1, 200).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres.");

            RuleFor(c => c.Cidade)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório.")
                .Length(1, 200).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres.");
        }
    }
}
