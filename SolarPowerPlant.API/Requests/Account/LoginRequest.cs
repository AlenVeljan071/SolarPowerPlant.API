using FluentValidation;
using SolarPowerPlant.API.Helpers;

namespace SolarPowerPlant.API.Requests.Account
{
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Email).SetValidator(new EmailAddressValidator());
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
