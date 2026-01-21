using FluentValidation;
using Snowly.Application.Queries.MessageQueries.GetMessagesUserMessaging;

namespace Snowly.WebAPI.Validations.MessageValidations.MessageQueryValidations
{
    public class GetMessagesUserMessagingValidator : AbstractValidator<GetMessagesUserMessagingQuery>
    {
        public GetMessagesUserMessagingValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("Kullanıcı Id zorunludur")
                .NotEqual(Guid.Empty).WithMessage("Geçerli bir kullanıcı Id giriniz");
        }
    }
}
