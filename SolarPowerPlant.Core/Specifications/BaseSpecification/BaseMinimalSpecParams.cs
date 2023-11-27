namespace SolarPowerPlant.Core.Specifications.BaseSpecification
{
    public class BaseMinimalSpecParams
    {
        private const int MaxPageSize = 25;

        public int PageIndex { get; set; } = 1;

        private int _pageSize = 25;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
        }

        public int? UserId { get; set; }


    }
}
