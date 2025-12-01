using FluentValidation;
using Snowly.Application.Commands.RefreshTokenCommands.CheckRefreshToken;

namespace Snowly.WebAPI.Validations.RefreshTokenValidations
{
    public class CheckRefreshTokenCommandValidator : AbstractValidator<CheckRefreshTokenCommand>
    {
        public CheckRefreshTokenCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotNull().WithMessage("Kullanıcı Id' zorunludur")
                .NotEqual(Guid.Empty).WithMessage("Geçerli Kullanıcı Id' giriniz");
            RuleFor(x => x.RefreshToken)
                .NotEmpty().WithMessage("Yenileme tokeni boş olamaz");
        }
    }
}
