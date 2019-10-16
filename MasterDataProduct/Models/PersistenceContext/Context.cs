using MasterDataProduct.Models.Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace MasterDataProduct.Models.PersistenceContext
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }
        
        public DbSet<Product> Products { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>()
                .OwnsOne(p => p.Plan);
            modelBuilder.Entity<Product>(config =>
            {
                config.HasIndex(t => new {t.id, t.Ref})
                    .IsUnique(true);
                config.Property(t => t.Ref)
                    .HasConversion(new MachineTypeIdValueVConverter());
            });
        }
    }
}