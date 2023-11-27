using FluentValidation;

namespace SolarPowerPlant.API.Requests.Account
{
    public class RefreshTokenRequest
    {
        public string? RefreshToken { get; set; }
    }

    public class RefreshTokenRequestValidator : AbstractValidator<RefreshTokenRequest>
    {
        public RefreshTokenRequestValidator()
        {
            RuleFor(x => x.RefreshToken).NotEmpty();
        }
    }
}
