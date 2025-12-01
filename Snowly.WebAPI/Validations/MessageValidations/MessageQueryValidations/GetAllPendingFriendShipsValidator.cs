using FluentValidation;
using Snowly.Application.Queries.FriendShipQueries.GetAllPendingFriendShips;

namespace Snowly.WebAPI.Validations.MessageValidations.MessageQueryValidations
{
    public class GetAllPendingFriendShipsValidator : AbstractValidator<GetAllPendingFriendShipQuery>
    {
        public GetAllPendingFriendShipsValidator() 
        { 
            RuleFor(x => x.RequesterId)
                .NotEmpty().WithMessage("Arkadaşlık isteği gönderici kullanıcı Id zorunludur")
                .NotEqual(Guid.Empty).WithMessage("Geçerli bir arkadaşlık isteği gönderici kullanıcı Id giriniz");
        }
    }
}
