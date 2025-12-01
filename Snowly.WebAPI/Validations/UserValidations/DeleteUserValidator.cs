using FluentValidation;
using Snowly.Application.Commands.UserCommands.DeleteUser;

namespace Snowly.WebAPI.Validations.UserValidations
{
    public class DeleteUserValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserValidator() 
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-posta zorunludur")
                .EmailAddress().WithMessage("Geçerli bir E-posta adresi giriniz");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Şifre zorunludur");
        }
    }
}
