using FluentValidation;
using Snowly.Application.Commands.FriendShipCommands.DeleteFriendShip;

namespace Snowly.WebAPI.Validations.FriendShipValidations
{
    public class DeleteFriendShipValidator : AbstractValidator<DeleteFriendShipCommand>
    {
        public DeleteFriendShipValidator() 
        {
            RuleFor(x => x.RequesterId)
                .NotEmpty().WithMessage("Arkadaşlık isteği gönderen kullanıcı Id zorunludur")
                .NotEqual(Guid.Empty).WithMessage("Geçerli bir arkadaşlık isteği gönderen kullanıcı Id girilmelidir");

            RuleFor(x => x.AddresseeId)
                .NotEmpty().WithMessage("Arkadaşlık isteği alıcı kullanıcı Id zorunludur")
                .NotEqual(Guid.Empty).WithMessage("Geçerli bir arkadaşlık isteği alıcı kullanıcı Id girilmelidir");
        }
    }
}
