using BaseCosmosBaseHelper.Repository.Concrete;
using CosmosBase.Repository.Abstract;
using CosmosBase.Repository.Concrete;
using Microsoft.Extensions.DependencyInjection;

namespace CosmosBase
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCosmosBase(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
            services.AddHttpContextAccessor();
            return services;
        }
    }
}
