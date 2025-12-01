using FluentValidation;
using Snowly.Application.Queries.FriendShipQueries.GetAllPendingFriendShipsForAddressee;
using System.Data;

namespace Snowly.WebAPI.Validations.MessageValidations.MessageQueryValidations
{
    public class GetAllPendingFriendShipsForAddresseeValidator : AbstractValidator<GetAllPendingFriendShipsForAddresseeQuery>
    {
        public GetAllPendingFriendShipsForAddresseeValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("Kullanıcı Id zorunludur")
                .NotEqual(Guid.Empty).WithMessage("Geçerli bir kullanıcı Id giriniz");
        }
    }
}
