using Microsoft.EntityFrameworkCore;
using PruebaTecnicaDotNetApi7.Persistence;
using PruebaTecnicaDotNetApi7.Repository;
using PruebaTecnicaDotNetApi7.Services;

namespace PruebaTecnicaDotNetApi7.Extensions
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddDatabaseServices(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));
            services.AddScoped<DbInitializer>();

            // repositorios
            services.AddScoped<PurchaseOrderRepository>();
            services.AddScoped<PurchaseOrderItemRepository>();
            services.AddScoped<ProductRepository>();

            // servicios
            services.AddScoped<PurchaseOrderService>();
            services.AddScoped<PurchaseOrderItemService>();
            services.AddScoped<ProductService>();

            return services;
        }
        public static async Task<WebApplication> InitializeDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbInitializer = scope.ServiceProvider.GetRequiredService<DbInitializer>();
            await dbInitializer.InitializeAsync();
            return app;
        }
    }
}
