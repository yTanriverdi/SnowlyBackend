using FluentValidation;
using Snowly.Application.Queries.UserQueries.GetUserId;

namespace Snowly.WebAPI.Validations.UserValidations.UserQueryValidations
{
    public class GetUserIdValidator : AbstractValidator<GetUserIdQuery>
    {
        public GetUserIdValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("Kullanıcı Id zorunludur")
                .NotEqual(Guid.Empty).WithMessage("Geçerli bir kullanıcı Id girilmelidir");
        }
    }
}
