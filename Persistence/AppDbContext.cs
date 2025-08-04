using Microsoft.EntityFrameworkCore;

namespace PruebaTecnicaDotNetApi7.Persistence
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions options): base(options)
        {
        }
        public DbSet<Models.Product> Products { get; set; } = null!;
        public DbSet<Models.PurchaseOrder> PurchaseOrders { get; set; } = null!;
        public DbSet<Models.PurchaseOrderItem> PurchaseOrderItems { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // configuration for relationships and constraints for PurchaseOrderItem (including Product and PurchaseOrder)
            modelBuilder.Entity<Models.PurchaseOrderItem>()
                .HasOne(poi => poi.PurchaseOrder)
                .WithMany()
                .HasForeignKey(poi => poi.PurchaseOrderId);
            modelBuilder.Entity<Models.PurchaseOrderItem>()
                .HasOne(poi => poi.Product)
                .WithMany()
                .HasForeignKey(poi => poi.ProductId);
            modelBuilder.Entity<Models.PurchaseOrder>()
                .Property(po => po.TotalPriceUSD)
                .HasPrecision(18, 2); // Set precision for TotalPriceUSD
            modelBuilder.Entity<Models.Product>()
                .Property(p => p.PriceUSD)
                .HasPrecision(18, 2); // Set precision for PriceUSD
            modelBuilder.Entity<Models.PurchaseOrderItem>()
                .Property(poi => poi.UnitPrice)
                .HasPrecision(18, 2); // Set precision for UnitPrice
        }
    }
}
