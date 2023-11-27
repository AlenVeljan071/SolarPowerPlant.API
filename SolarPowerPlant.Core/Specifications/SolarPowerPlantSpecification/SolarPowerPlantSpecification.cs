using SolarPowerPlant.Core.Specifications.BaseSpecification;

namespace SolarPowerPlant.Core.Specifications.SolarPowerPlantSpecification
{
    public class SolarPowerPlantSpecification : BaseSpecification<Entities.SolarPowerPlant>
    {
        public SolarPowerPlantSpecification(SolarPowerPlantSpecParams specParams) : base(x =>
        (string.IsNullOrEmpty(specParams.Search1) || (x.Name!.ToLower().Contains(specParams.Search1))) &&
        (string.IsNullOrEmpty(specParams.Search2) || (x.Name!.ToLower().Contains(specParams.Search2))) &&
        (string.IsNullOrEmpty(specParams.Search) || (x.Name!.ToLower().Contains(specParams.Search))) &&
         (!specParams.UserId.HasValue || specParams.UserId == x.UserId) 
        )
        {
           


        }

        public SolarPowerPlantSpecification(int Id, int userId) : base(x =>
        (x.Id == Id && x.UserId == userId))
        {
           
        }
 
    }
}
