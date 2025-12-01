using FluentValidation;
using Snowly.Application.Commands.UserCommands.LoginUser;

namespace Snowly.WebAPI.Validations.UserValidations
{
    public class LoginUserValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserValidator() 
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-posta zorunludur")
                .EmailAddress().WithMessage("Geçerli bir E-posta adresi giriniz");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Şifre zorunludur");
        }
    }
}
