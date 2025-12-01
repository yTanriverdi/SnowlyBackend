using FluentValidation;
using Snowly.Application.Commands.MessageCommands.CreateMessage;

namespace Snowly.WebAPI.Validations.MessageValidations
{
    public class CreateMessageValidator : AbstractValidator<CreateMessageCommand>
    {
        public CreateMessageValidator() 
        { 
            RuleFor(x => x.SenderId)
                .NotEmpty().WithMessage("Gönderici Id zorunludur")
                .NotEqual(Guid.Empty).WithMessage("Geçerli bir gönderici Id girilmelidir");
            RuleFor(x => x.ReceiverId)
                .NotEmpty().WithMessage("Alıcı Id zorunludur")
                .NotEqual(Guid.Empty).WithMessage("Geçerli bir alıcı Id girilmelidir");
            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Mesaj içeriği zorunludur")
                .MaximumLength(500).WithMessage("Mesaj 500 karakterden uzun olamaz");
        }
    }
}
