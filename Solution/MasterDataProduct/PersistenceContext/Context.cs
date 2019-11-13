using MasterDataProduct.Models.Products;
using Microsoft.EntityFrameworkCore;

namespace MasterDataProduct.PersistenceContext
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ManufacturingPlan> ManufacturingPlans { get; set; }
        public virtual DbSet<OperationId> OperationIds { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new ManufacturingPlanConfiguration());
            modelBuilder.ApplyConfiguration(new OperationIdConfiguration());
            
            base.OnModelCreating(modelBuilder);
        }
    }
}