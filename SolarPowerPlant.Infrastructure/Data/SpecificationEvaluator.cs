using Microsoft.EntityFrameworkCore;
using SolarPowerPlant.Core.Entities;
using SolarPowerPlant.Core.Specifications.BaseSpecification;

namespace SolarPowerPlant.Infrastructure.Data
{
    public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)
        {
            var query = inputQuery;

            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }
           
            if (spec.OrderBy != null)
            {
                var baseSpec = spec as BaseSpecification<TEntity>;
                query = baseSpec!.OrderByMultiple(query, baseSpec.OrderBy);
            }

            if (spec.GroupBy != null)
            {
                query = query.GroupBy(spec.GroupBy).SelectMany(x => x);
            }

            if (spec.IsPagingEnabled)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
            }

            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));
            
            query = spec.IncludesStrings.Aggregate(query, (current, include) => current.Include(include));


            return query;
        }
    }
}
