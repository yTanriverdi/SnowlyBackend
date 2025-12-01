using FluentValidation;
using Snowly.Application.Commands.MessageCommands.MarkReadMessage;

namespace Snowly.WebAPI.Validations.MessageValidations
{
    public class MarkReadMessageValidator : AbstractValidator<MarkReadMessageCommand>
    {
        public MarkReadMessageValidator() 
        {
            RuleFor(x => x.ReceiverId)
                .NotEmpty().WithMessage("Alıcı Id zorunludur")
                .NotEqual(Guid.Empty).WithMessage("Geçerli bir alıcı Id girilmelidir");
            
            RuleFor(x => x.SenderId)
                .NotEmpty().WithMessage("Gönderici Id zorunludur")
                .NotEqual(Guid.Empty).WithMessage("Geçerli bir gönderici Id girilmelidir");
        }
    }
}
