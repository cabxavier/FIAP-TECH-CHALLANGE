using CORE.Entity;
using FluentValidation;

namespace CORE.Validator
{
    public class ContatoValidator : AbstractValidator<Contato>
    {
        public ContatoValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage("Informe o nome.")
                .Length(3, 200)
                .WithMessage("Nome deve ter de 1 e/ou 100 caracteres.");
            RuleFor(x => x.Telefone)
                .NotEmpty()
                .WithMessage("Informe o telefone.")
                .Matches("[0-9]")
                .WithMessage("Telefone deve ser informado somente números.")
                .Matches(@"^\d{10,11}$")
                .WithMessage("Telefone deve ter 10 e/ou 11 dígitos.");
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Informe o e-mail.")
                .EmailAddress()
                .WithMessage("Informe um e-mail válido.");
        }
    }
}
