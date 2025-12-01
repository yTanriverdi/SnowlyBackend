using FluentValidation;
using Snowly.Application.Queries.FriendShipQueries.GetAllAcceptedFriendShips;

namespace Snowly.WebAPI.Validations.MessageValidations.MessageQueryValidations
{
    public class GetAllAcceptedFriendShipValidator : AbstractValidator<GetAllAcceptedFriendShipQuery>
    {
        public GetAllAcceptedFriendShipValidator() 
        {
            RuleFor(x => x.RequesterId)
                .NotEmpty().WithMessage("Arkadaşlık isteği gönderici kullanıcı Id zorunludur")
                .NotEqual(Guid.Empty).WithMessage("Geçerli bir arkadaşlık isteği gönderici kullanıcı Id giriniz");
        }
    }
}
