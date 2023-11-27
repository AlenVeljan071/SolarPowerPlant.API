using Microsoft.EntityFrameworkCore.Storage;
using SolarPowerPlant.Core.Entities;
using SolarPowerPlant.Core.Interfaces;
using SolarPowerPlant.Infrastructure.Data.Context;
using SolarPowerPlant.Infrastructure.Data.Repository;
using System.Collections;

namespace SolarPowerPlant.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly AppDbContext _appDbContext;
        private Hashtable? _repositories;
        public UnitOfWork(AppDbContext writeContext)
        {
            _appDbContext = writeContext;
        }

        public async Task<int> Complete()
        {
            return await _appDbContext.SaveChangesAsync();

        }

        public void Dispose()
        {

            _appDbContext.Dispose();
        }

        public IGenericRepository<TEntity>? Repository<TEntity>() where TEntity : BaseEntity
        {
            if (_repositories == null) _repositories = new Hashtable();

            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(GenericRepository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _appDbContext);

                _repositories.Add(type, repositoryInstance);
            }

            return _repositories[type] as IGenericRepository<TEntity>;
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _appDbContext.Database.BeginTransactionAsync();
        }

    }
}
