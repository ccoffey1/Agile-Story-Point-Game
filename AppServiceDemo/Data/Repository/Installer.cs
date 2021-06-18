using AppServiceDemo.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AppServiceDemo.Data.Repository
{
    public static class Installer
    {
        public static IServiceCollection AddContextAndRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationContext>(config =>
            {
                config.UseSqlServer(configuration.GetConnectionString("MsSqlConnStr"), options =>
                {
                    options.EnableRetryOnFailure();
                });
            }, ServiceLifetime.Transient);

            #region Repositories
            services.AddTransient<IPlayerRepository, PlayerRepository>();
            services.AddTransient<IGameSessionRepository, GameSessionRepository>();
            #endregion

            return services;
        }
    }
}
