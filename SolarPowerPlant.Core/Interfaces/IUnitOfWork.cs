using Microsoft.EntityFrameworkCore.Storage;
using SolarPowerPlant.Core.Entities;

namespace SolarPowerPlant.Core.Interfaces
{


    public interface IUnitOfWork : IDisposable
        {
            IGenericRepository<TEntity>? Repository<TEntity>() where TEntity : BaseEntity;
            Task<int> Complete();
            Task<IDbContextTransaction> BeginTransactionAsync();
    }
    
}
