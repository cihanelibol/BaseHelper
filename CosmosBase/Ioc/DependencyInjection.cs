using BaseCosmosBaseHelper.Repository.Concrete;
using CosmosBase.Repository.Abstract;
using CosmosBase.Repository.Concrete;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CosmosBase
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCosmosBase(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
            services.AddHttpContextAccessor();

            var jwtSettings = configuration.GetSection("Jwt");
            if (jwtSettings.Exists())
            {
                var key = Encoding.UTF8.GetBytes(jwtSettings["SecurityKey"]);

                services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }).AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings["Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ClockSkew = TimeSpan.Zero
                    };
                });
            }
           

            return services;
        }
    }
}
