using FluentValidation;
using Snowly.Application.Commands.FriendShipCommands.AcceptFriendShip;

namespace Snowly.WebAPI.Validations.FriendShipValidations
{
    public class AcceptFriendShipValidator : AbstractValidator<AcceptFriendShipCommand>
    {
        public AcceptFriendShipValidator() 
        { 
            RuleFor(x => x.FriendShipId)
                .NotEmpty().WithMessage("Arkadaşlık isteği Id zorunludur")
                .NotEqual(Guid.Empty).WithMessage("Geçerli bir arkadaşlık isteği Id girilmelidir");
        }
    }
}
