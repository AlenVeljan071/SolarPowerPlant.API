using SolarPowerPlant.Core.Entities;
using SolarPowerPlant.Core.Specifications.BaseSpecification;
using System.Linq.Expressions;


namespace SolarPowerPlant.Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(int id);
        Task<IList<T>> ListAllAsync();
        Task<T?> GetEntityWithSpec(ISpecification<T> spec);
        Task<IList<T>> ListAsync(ISpecification<T> spec);
        Task<TProjection> GetFirstAsync<TProjection>(Expression<Func<T, bool>> filter, Expression<Func<T, TProjection>> projection , Expression<Func<T, object>>? orderBy = null, Expression<Func<T, object>>? orderByDesc = null);
        Task<List<TProjection>> GetAsync<TProjection>(Expression<Func<T, bool>> filter, Expression<Func<T, TProjection>> projection);
        Task<int> CountAsync(ISpecification<T> spec);
        void Add(T entity);
        void AddRange(List<T> entities);
        void Update(T entity);
        void UpdateRange(IEnumerable<T> entities);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);
    }
}
