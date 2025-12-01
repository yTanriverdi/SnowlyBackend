using FluentValidation;
using Snowly.Application.Queries.UserQueries.GetUserEmail;

namespace Snowly.WebAPI.Validations.UserValidations.UserQueryValidations
{
    public class GetUserEmailValidator : AbstractValidator<GetUserEmailQuery>
    {
        public GetUserEmailValidator() 
        { 
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-posta zorunludur")
                .EmailAddress().WithMessage("Geçerli bir E-posta adresi girin");
        }
    }
}
