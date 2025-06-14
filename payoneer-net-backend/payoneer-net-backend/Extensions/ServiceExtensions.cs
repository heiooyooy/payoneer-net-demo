using payoneer_net_backend.DbContexts;
using Microsoft.EntityFrameworkCore;
using payoneer_net_backend.Middleware;

namespace payoneer_net_backend.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddCustomServices(this IServiceCollection services, IConfiguration configuration)
    {
        return services;
    }

    public static void MigrateAndSeedData(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<OrderDbContext>();
                context.Database.Migrate();
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred while migrating or seeding the database.");
            }
        }
    }

    public static void AddCustomMiddleware(this WebApplication app)
    {
        app.UseMiddleware<ErrorHandlingMiddleware>();
    }

}