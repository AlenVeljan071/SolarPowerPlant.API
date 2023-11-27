using FluentValidation;
using SolarPowerPlant.API.Helpers;
using SolarPowerPlant.Core.Entities;
using SolarPowerPlant.Core.Enums;

namespace SolarPowerPlant.API.Requests.Account
{
    public class SignUserAccountRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public AddressEntity Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Password { get; set; }
    }
    public class SignUserAccountRequestValidator : AbstractValidator<SignUserAccountRequest>
    {
        public SignUserAccountRequestValidator()
        {
            RuleFor(x => x.FirstName).SetValidator(new FirstNameValidator());
            RuleFor(x => x.LastName).SetValidator(new LastNameValidator());
            RuleFor(x => x.Email).SetValidator(new EmailAddressValidator());
            RuleFor(x => x.Phone).SetValidator(new PhoneValidator());
            RuleFor(x => x.Address).SetValidator(new AddressEntityValidator());
            RuleFor(x => x.Password).NotEmpty();
            RuleFor(x => x.DateOfBirth).SetValidator(new DateTimeValidator());
        }
    }
}
