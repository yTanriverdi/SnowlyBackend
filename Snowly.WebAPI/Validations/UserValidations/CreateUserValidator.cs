using FluentValidation;
using Snowly.Application.Commands.UserCommands.CreateUser;

namespace Snowly.WebAPI.Validations.UserValidations
{
    public class CreateUserValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserValidator()
        {
            RuleFor(x => x.Email)
                 .NotEmpty().WithMessage("E-posta zorunludur")
                 .EmailAddress().WithMessage("Geçerli bir E-posta adresi girin");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Şifre zorunludur")
                .MinimumLength(5).WithMessage("Şifre en az 5 karakter uzunluğunda olabilir")
                .MaximumLength(30).WithMessage("Şifre en fazla 30 karakter uzunluğunda olabilir");
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("İsim zorunludur")
                .MinimumLength(3).WithMessage("İsim en az 3 karakter uzunluğunda olabilir")
                .Matches("^[a-zA-ZğüşıöçĞÜŞİÖÇ ]+$").WithMessage("İsim yalnızca harf içermelidir");
            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Soyad zorunludur")
                .MinimumLength(3).WithMessage("Soyad en az 3 karakter uzunluğunda olabilir")
                .Matches("^[a-zA-ZğüşıöçĞÜŞİÖÇ ]+$").WithMessage("Soyad yalnızca harf içermelidir");
        }
    }
}
