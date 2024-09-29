using BaseHelper.Entites;
using Microsoft.EntityFrameworkCore;

namespace BaseHelper.Repository.Abstract
{
    public interface IUnitOfWork<TContext> : IDisposable where TContext : DbContext
    {
        TContext Context { get; }
        Task<int> CompleteAsync();
    }
}
