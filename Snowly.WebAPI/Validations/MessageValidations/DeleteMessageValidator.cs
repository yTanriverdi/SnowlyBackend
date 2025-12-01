using FluentValidation;
using Snowly.Application.Commands.MessageCommands.DeleteMessage;

namespace Snowly.WebAPI.Validations.MessageValidations
{
    public class DeleteMessageValidator : AbstractValidator<DeleteMessageCommand>
    {
        public DeleteMessageValidator() 
        {
            RuleFor(x => x.MessageId)
                .NotEmpty().WithMessage("Mesaj Id zorunludur")
                .NotEqual(Guid.Empty).WithMessage("Geçerli bir mesaj Id girilmelidir");
        }
    }
}
