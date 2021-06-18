using Microsoft.Extensions.DependencyInjection;

namespace AppServiceDemo.Service
{
    public static class Installer
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IPlayerService, PlayerService>();
            services.AddTransient<IGameSessionService, GameSessionService>();

            return services;
        }
    }
}
