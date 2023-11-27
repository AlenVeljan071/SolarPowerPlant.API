using SolarPowerPlant.Core.Entities;
using SolarPowerPlant.Core.Specifications.BaseSpecification;
using System.Linq.Expressions;

namespace SolarPowerPlant.Core.Interfaces.Repositories
{
    public  interface IReadGenericRepository<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<T?> GetEntityWithSpec(ISpecification<T> spec);
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
        Task<int> CountAsync(ISpecification<T> spec);
        Task<List<TProjection>> GetAsync<TProjection>(Expression<Func<T, bool>> filter, Expression<Func<T, TProjection>> projection);
        Task<IReadOnlyList<TProjection>> ListAsync<TProjection>(ISpecification<T> spec,Expression<Func<T, TProjection>> projection);
        Task<TProjection> GetFirstAsync<TProjection>(Expression<Func<T, bool>> filter, Expression<Func<T, TProjection>> projection, Expression<Func<T, object>>? orderBy = null, Expression<Func<T, object>>? orderByDesc = null);
    }
}
