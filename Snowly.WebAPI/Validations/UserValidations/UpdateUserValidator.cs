using FluentValidation;
using Snowly.Application.Commands.UserCommands.UpdateUser;

namespace Snowly.WebAPI.Validations.UserValidations
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserValidator() 
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-posta zorunludur")
                .EmailAddress().WithMessage("Geçerli bir E-posta giriniz");
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("İsim zorunludur")
                .MinimumLength(3).WithMessage("İsim en az 3 karakter uzunluğunda olabilir")
                .Matches("^[a-zA-ZğüşıöçĞÜŞİÖÇ ]+$").WithMessage("İsim yalnızca harf içermelidir");
            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Soyad zorunludur")
                .MinimumLength(3).WithMessage("Soyad en az 2 karakter uzunluğunda olabilir")
                .Matches("^[a-zA-ZğüşıöçĞÜŞİÖÇ ]+$").WithMessage("Soyad yalnızca harf içermelidir");
        }
    }
}
