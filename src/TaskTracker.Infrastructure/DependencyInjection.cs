using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using TaskTracker.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace TaskTracker.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>((sp, options) =>
            {
                options.UseNpgsql(connectionString);
            });

            return services;
        }
    }
}