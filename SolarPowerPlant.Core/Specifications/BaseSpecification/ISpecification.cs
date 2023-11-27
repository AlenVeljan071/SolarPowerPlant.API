using System.Linq.Expressions;

namespace SolarPowerPlant.Core.Specifications.BaseSpecification
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>>? Criteria { get; }
        List<Expression<Func<T, object>>> Includes { get; }
        List<string> IncludesStrings { get; }
        List<Tuple<Expression<Func<T, object>>, bool>> OrderBy { get; set; }
        Expression<Func<T, object>>? GroupBy { get; set; }
        int Take { get; }
        int Skip { get; }
        bool IsPagingEnabled { get; }
    }
}
