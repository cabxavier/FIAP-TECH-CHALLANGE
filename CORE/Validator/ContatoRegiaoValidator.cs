using CORE.Entity;
using FluentValidation;

namespace CORE.Validator
{
    public class ContatoRegiaoValidator : AbstractValidator<ContatoRegiao>
    {
        public ContatoRegiaoValidator()
        {
            RuleFor(x => x.ContatoId)
                .NotEmpty()
                .WithMessage("Informe o contato.");
            RuleFor(x => x.RegiaoId)
                .NotEmpty()
                .WithMessage("Informe a região.");
        }
    }
}
