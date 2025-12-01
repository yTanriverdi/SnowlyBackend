using FluentValidation;
using Snowly.Application.Commands.ConfirmCodeCommands.ConfirmCode;

namespace Snowly.WebAPI.Validations.ConfirmCodeValidations
{
    public class ConfirmCodeValidator : AbstractValidator<ConfirmCodeCommand>
    {
        public ConfirmCodeValidator() 
        {
            RuleFor(x => x.Email)
                 .NotEmpty().WithMessage("E-posta zorunludur")
                 .EmailAddress().WithMessage("Geçerli bir E-posta adresi girin");
            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("Onay kodu zorunludur")
                .MinimumLength(6).WithMessage("Onay kodu 6 hanelidir")
                .MaximumLength(6).WithMessage("Onay kodu 6 hanelidir");
        }
    }
}
