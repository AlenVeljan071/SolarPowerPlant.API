using FluentValidation;
using SolarPowerPlant.API.Helpers;
using SolarPowerPlant.Core.Entities;

namespace SolarPowerPlant.API.Requests.PowerPlant
{
    public class SolarPowerPlantRequest
    {
        public  string? Name { get; set; }
        public int InstalledPower { get; set; }
        public required DateTime InstallationDate { get; set; }
        public required AddressEntity Address { get; set; }
        public required decimal Latitude { get; set; }
        public required decimal Longitude { get; set; }
    }

    public class SolarPowerPlantRequestValidator : AbstractValidator<SolarPowerPlantRequest>
    {
        public SolarPowerPlantRequestValidator()
        {
            RuleFor(x => x.InstalledPower).NotEmpty();
            RuleFor(x => x.InstallationDate).SetValidator(new DateTimeValidator());
            RuleFor(x => x.Address).SetValidator(new AddressEntityValidator());
            RuleFor(x => x.Latitude).NotEmpty().PrecisionScale(12,9,false);
            RuleFor(x => x.Longitude).NotEmpty().PrecisionScale(12, 9, false);
        }
    }
}
