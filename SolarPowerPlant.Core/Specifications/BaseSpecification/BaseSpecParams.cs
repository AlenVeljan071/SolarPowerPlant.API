namespace SolarPowerPlant.Core.Specifications.BaseSpecification
{
    public class BaseSpecParams
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
        public string? Sort { get; set; }


        private string? _search;

        public string? Search
        {
            get => _search;
            set => _search = value?.ToLower();

        }
        private string? _search1;

        public string? Search1
        {
            get => _search1;
            set => _search1 = value?.ToLower();

        }
        private string? _search2;

        public string? Search2
        {
            get => _search2;
            set => _search2 = value?.ToLower();

        }
    }
}
