using FluentValidation;
using Snowly.Application.Commands.ConfirmCodeCommands.RefreshCode;

namespace Snowly.WebAPI.Validations.ConfirmCodeValidations
{
    public class RefreshCodeValidator : AbstractValidator<RefreshCodeCommand>
    {
        public RefreshCodeValidator() 
        {
            RuleFor(x => x.Email)
                 .NotEmpty().WithMessage("E-posta zorunludur")
                 .EmailAddress().WithMessage("Geçerli bir E-posta adresi girin");
        }
    }
}
