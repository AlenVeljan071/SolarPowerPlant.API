using Microsoft.EntityFrameworkCore;
using SolarPowerPlant.Core.Entities;
using SolarPowerPlant.Core.Interfaces;
using SolarPowerPlant.Core.Specifications.BaseSpecification;
using SolarPowerPlant.Infrastructure.Data.Context;
using System.Linq.Expressions;


namespace SolarPowerPlant.Infrastructure.Data.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        
        protected readonly AppDbContext _context;
        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void AddRange(List<T> entities)
        {
            _context.Set<T>().AddRange(entities);
        }

        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
        public void DeleteRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        public void DeleteRange(List<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        public async Task<List<TProjection>> GetAsync<TProjection>(Expression<Func<T, bool>> filter, Expression<Func<T, TProjection>> projection)
        {
            return await _context.Set<T>().Where(filter).Select(projection).ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
     
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T?> GetEntityWithSpec(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<TProjection> GetFirstAsync<TProjection>(Expression<Func<T, bool>> filter, Expression<Func<T, TProjection>> projection, Expression<Func<T, object>>? orderBy = null, Expression<Func<T, object>>? orderByDesc = null)
        {
            var query =  _context.Set<T>().Where(filter);
            if (orderBy != null) query = query.OrderBy(orderBy);
            if (orderByDesc != null) query = query.OrderByDescending(orderByDesc);
            return await query.Select(projection).FirstOrDefaultAsync();
        }

        public async Task<IList<T>> ListAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<IList<T>> ListAsync(ISpecification<T> spec)
        {
            var query = ApplySpecification(spec);

            return await query.ToListAsync();
        }

        public void Update(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            foreach(var entity in entities)
            {
                _context.Set<T>().Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;
            }
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
        }
    }
}
