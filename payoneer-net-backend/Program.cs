using Microsoft.EntityFrameworkCore;
using payoneer_net_backend.DbContexts;
using payoneer_net_backend.Extensions;
using Serilog;

namespace payoneer_net_backend;

public partial class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Configure logging
        builder.Host.UseSerilog((context, config) => { config.ReadFrom.Configuration(context.Configuration); });

        // Add custom services
        builder.Services.AddCustomServices();

        // Configure database
        builder.Services.AddDbContext<OrderDbContext>(options =>
            options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
        );

        var app = builder.Build();

        // Configure the HTTP request pipeline
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        // Initialize database
        app.MigrateAndSeedData();
        app.AddCustomMiddleware();

        app.Run();
    }
}