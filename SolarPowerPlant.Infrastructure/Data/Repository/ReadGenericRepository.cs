using Microsoft.EntityFrameworkCore;
using SolarPowerPlant.Core.Entities;
using SolarPowerPlant.Core.Interfaces.Repositories;
using SolarPowerPlant.Core.Specifications.BaseSpecification;
using SolarPowerPlant.Infrastructure.Data.Context;
using System.Linq.Expressions;


namespace SolarPowerPlant.Infrastructure.Data.Repository
{
    public class ReadGenericRepository<T> : IReadGenericRepository<T> where T : BaseEntity
    {
        protected readonly AppDbContext _context;

        public ReadGenericRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        public async Task<List<TProjection>> GetAsync<TProjection>(Expression<Func<T, bool>> filter, Expression<Func<T, TProjection>> projection)
        {
            return await _context.Set<T>().AsNoTracking().Where(filter).Select(projection).ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T?> GetEntityWithSpec(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<T?> GetEntityWithSpec(int id, ISpecification<T> spec)
        {
            return await ApplySpecification(spec).Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            var query = ApplySpecification(spec);

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<IReadOnlyList<TProjection>> ListAsync<TProjection>(ISpecification<T> spec, Expression<Func<T, TProjection>> projection)
        {
            var query = ApplySpecification(spec);

            return await query.AsNoTracking().Select(projection).ToListAsync();
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
        }
        public async Task<TProjection> GetFirstAsync<TProjection>(Expression<Func<T, bool>> filter, Expression<Func<T, TProjection>> projection, Expression<Func<T, object>>? orderBy = null, Expression<Func<T, object>>? orderByDesc = null)
        {
            var query = _context.Set<T>().AsNoTracking().Where(filter);
            if (orderBy != null) query = query.OrderBy(orderBy);
            if (orderByDesc != null) query = query.OrderByDescending(orderByDesc);
            return await query.Select(projection).FirstOrDefaultAsync();
        }
    }
}
