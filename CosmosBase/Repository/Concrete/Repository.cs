using CosmosBase.Entites;
using CosmosBase.Repository.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Security.Claims;


namespace CosmosBase.Repository.Concrete
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;
        private readonly IHttpContextAccessor httpContextAccesor;

        public Repository(DbContext context, IHttpContextAccessor httpContextAccesor)
        {
            _context = context;
            _dbSet = context.Set<T>();
            this.httpContextAccesor = httpContextAccesor;
        }

        private Guid GetUserId()
        {
            string userId = httpContextAccesor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Guid.Parse(userId == null ? Guid.Empty.ToString() : userId);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            entity.SetCreatedAt(DateTime.UtcNow);
            entity.SetCreatedBy(GetUserId());
            await _dbSet.AddAsync(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            entity.SetUpdatedAt(DateTime.UtcNow);
            entity.SetUpdatedBy(GetUserId());
            _dbSet.Update(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            entity.SetIsDeleted(true);
            _dbSet.Update(entity);
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }
        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

    }
}
