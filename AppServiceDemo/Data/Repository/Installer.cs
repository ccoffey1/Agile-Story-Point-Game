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
            // Cosmos DB
            services.AddDbContext<CosmosDbContext>(options => options.UseCosmos(
                configuration.GetValue<string>("Cosmos:EndpointUri"),
                configuration.GetValue<string>("Cosmos:PrimaryKey"),
                databaseName: "PlanningPokerDB"));

            #region Repositories
            services.AddTransient<IPlayerRepository, PlayerRepository>();
            services.AddTransient<IGameSessionRepository, GameSessionRepository>();
            #endregion

            return services;
        }
    }
}
