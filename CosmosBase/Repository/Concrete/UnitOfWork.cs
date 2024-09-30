using CosmosBase.Entites;
using CosmosBase.Repository.Abstract;
using CosmosBase.Repository.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace BaseCosmosBaseHelper.Repository.Concrete
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : DbContext
    {
        private readonly TContext _context;
        private readonly Dictionary<Type, object> _repositories = new();
        private readonly IHttpContextAccessor httpContextAccesor;

        public UnitOfWork(TContext context, IHttpContextAccessor httpContextAccesor )
        {
            _context = context;
            this.httpContextAccesor = httpContextAccesor;
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity
        {
            if (_repositories.TryGetValue(typeof(TEntity), out var repository))
            {
                return (IRepository<TEntity>)repository;
            }

            var newRepository = new Repository<TEntity>(_context, httpContextAccesor);
            _repositories[typeof(TEntity)] = newRepository;
            return newRepository;
        }

        public TContext Context => _context; 

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

}
