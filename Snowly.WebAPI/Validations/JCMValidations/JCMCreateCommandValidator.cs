using FluentValidation;
using Snowly.Application.Commands.UserCommands.JCM;

namespace Snowly.WebAPI.Validations.JCMValidations
{
    public class JCMCreateCommandValidator : AbstractValidator<JCMCreateCommand>
    {
        public JCMCreateCommandValidator() 
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("Kullanıcı Id zorunludur")
                .NotEqual(Guid.Empty).WithMessage("Geçerli bir kullanıcı Id giriniz");

            RuleFor(x => x.JcmToken)
                .NotEmpty().WithMessage("JcmToken zorunludur");
        }
    }
}
