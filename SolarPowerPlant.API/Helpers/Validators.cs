using FluentValidation;
using SolarPowerPlant.Core.Entities;

namespace SolarPowerPlant.API.Helpers
{
    public class AddressEntityValidator : AbstractValidator<AddressEntity>
    {
        public AddressEntityValidator()
        {
            RuleFor(x => x.Address).NotEmpty().WithMessage("Address must not be empty.").Length(0, 100).WithMessage("Address should have 100 chars at most.");
            RuleFor(x => x.City).Length(0, 50).WithMessage("City should have 50 chars at most.");
            RuleFor(x => x.Country).Length(0, 45).WithMessage("Country should have 45 chars at most.");
        }
    }

    public class DateTimeValidator : AbstractValidator<DateTime>
    {
        public DateTimeValidator()
        {
            RuleFor(x => x).Cascade(CascadeMode.Stop).NotEmpty()
                .Must(date => date != default(DateTime))
                .WithMessage("{PropertyName} is required");
        }
    }

    public class EmailAddressValidator : AbstractValidator<string>
    {
        public EmailAddressValidator()
        {
            RuleFor(x => x).NotEmpty().WithMessage("Email must not be empty.").Length(0, 100).WithMessage("Email should have 100 chars at most.").EmailAddress().WithMessage("Email not valid format.");
        }
    }
    public class NameValidator : AbstractValidator<string>
    {
        public NameValidator()
        {
            RuleFor(x => x).NotEmpty().WithMessage("Name must not be empty.").Length(0, 100).WithMessage("Name should have 100 chars at most.");
        }

    }
    public class FirstNameValidator : AbstractValidator<string>
    {
        public FirstNameValidator()
        {
            RuleFor(x => x).NotEmpty().WithMessage("First name must not be empty.").Length(0, 100).WithMessage("First name should have 100 chars at most.");
        }

    }
    public class LastNameValidator : AbstractValidator<string>
    {
        public LastNameValidator()
        {
            RuleFor(x => x).NotEmpty().WithMessage("Last name must not be empty.").Length(0, 100).WithMessage("Last name should have 100 chars at most.");
        }

    }
    public class PhoneValidator : AbstractValidator<string>
    {
        public PhoneValidator()
        {
            RuleFor(x => x).Length(0, 100).WithMessage("Phone should have 100 chars at most.");
        }
    }
}
