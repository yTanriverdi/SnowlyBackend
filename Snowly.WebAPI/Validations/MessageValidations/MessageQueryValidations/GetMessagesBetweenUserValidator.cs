using FluentValidation;
using Snowly.Application.Queries.MessageQueries.GetMessagesBetweenUser;

namespace Snowly.WebAPI.Validations.MessageValidations.MessageQueryValidations
{
    public class GetMessagesBetweenUserValidator : AbstractValidator<GetMessagesBetweenUserQuery>
    {
        public GetMessagesBetweenUserValidator() 
        {
            RuleFor(x => x.SenderId)
                .NotEmpty().WithMessage("Gönderici Id zorunludur")
                .NotEqual(Guid.Empty).WithMessage("Geçerli bir gönderici Id girilmelidir");
            RuleFor(x => x.ReceiverId)
                .NotEmpty().WithMessage("Alıcı Id zorunludur")
                .NotEqual(Guid.Empty).WithMessage("Geçerli bir alıcı Id girilmelidir");
            RuleFor(x => x.MessageSize)
                .GreaterThanOrEqualTo(30).WithMessage("Mesaj sayısı 30'dan az olamaz");
            RuleFor(x => x.MessageStack)
                .GreaterThanOrEqualTo(1).WithMessage("Mesaj isteği 1'den az olamaz");
        }
    }
}
