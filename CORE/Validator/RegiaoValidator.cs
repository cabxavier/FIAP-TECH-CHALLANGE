using CORE.Entity;
using FluentValidation;

namespace CORE.Validator
{
    public class RegiaoValidator : AbstractValidator<Regiao>
    {
        public RegiaoValidator()
        {
            RuleFor(x => x.Ddd)
                .NotEmpty()
                .WithMessage("Informe o ddd.")
                .Matches("[0-9]").WithMessage("Ddd deve ser informado somente números.")
                .Matches(@"^\d{2}$").WithMessage("Telefone deve ter 2 dígitos.");
        }
    }
}
