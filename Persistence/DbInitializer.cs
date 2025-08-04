using Microsoft.EntityFrameworkCore;

namespace PruebaTecnicaDotNetApi7.Persistence
{
    public class DbInitializer
    {
        private readonly AppDbContext _context;
        private readonly ILogger<DbInitializer> _logger;
        public DbInitializer(AppDbContext context, ILogger<DbInitializer> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task InitializeAsync()
        {
            try
            {
                var pendingMigrations = await _context.Database.GetPendingMigrationsAsync();

                if (pendingMigrations.Any())
                {
                    _logger.LogInformation($"Applying {pendingMigrations.Count()} pending migrations...");
                    await _context.Database.MigrateAsync();
                    _logger.LogInformation("Database migrations applied successfully.");
                }
                else
                {
                    _logger.LogInformation("Database is up to date - no migrations needed.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while initializing the database.");
                throw;
            }
        }
    }
}
