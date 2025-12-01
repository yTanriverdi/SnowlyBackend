using FluentValidation;
using Snowly.Application.Commands.FriendShipCommands.CreateFriendShip;

namespace Snowly.WebAPI.Validations.FriendShipValidations
{
    public class CreateFriendShipValidator : AbstractValidator<CreateFriendShipCommand>
    {
        public CreateFriendShipValidator() 
        { 
            RuleFor(x => x.RequesterId)
                .NotEmpty().WithMessage("Arkadaşlık isteği gönderen kullanıcı Id zorunludur")
                .NotEqual(Guid.Empty).WithMessage("Geçerli bir kullanıcı Id girilmelidir");

            RuleFor(x => x.AddresseeId)
                .NotEmpty().WithMessage("Arkadaşlık isteği alıcı kullanıcı Id zorunludur")
                .NotEqual(Guid.Empty).WithMessage("Geçerli bir kullanıcı Id girilmelidir");
        }
    }
}
