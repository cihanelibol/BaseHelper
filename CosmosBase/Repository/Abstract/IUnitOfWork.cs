using CosmosBase.Entites;
using Microsoft.EntityFrameworkCore;

namespace CosmosBase.Repository.Abstract
{
    public interface IUnitOfWork<TContext> : IDisposable where TContext : DbContext
    {
        TContext Context { get; }
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity;
        Task<int> SaveChangesAsync();
    }
}
