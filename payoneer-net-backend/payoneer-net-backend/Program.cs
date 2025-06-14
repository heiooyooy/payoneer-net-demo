using Microsoft.EntityFrameworkCore;
using payoneer_net_backend.DbContexts;
using payoneer_net_backend.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.UseSerilog((context, config) => { config.ReadFrom.Configuration(context.Configuration); });
builder.Services.AddControllers();

// var connectionString = builder.Configuration.GetConnectionString("PostgresConnection");

// use Sqlite for simplicity
builder.Services.AddDbContext<OrderDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MigrateAndSeedData();
app.AddCustomMiddleware();
app.MapControllers();

// no need to use Https for demo purpose
// app.UseHttpsRedirection();

app.Run();