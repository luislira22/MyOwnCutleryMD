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

            /* E preciso usar isto quando utilizamos Value-Objects se esses VO nao fores chave
             modelBuilder.Entity<Machine>()
                .OwnsOne(p => p.MachineId);*/
            modelBuilder.Entity<Product>()
                .OwnsOne(p => p.Plan);
            modelBuilder.Entity<Product>()
                .Property(o => o.Id)
                .HasConversion(new ProductIdValueVConverter());
        }
    }
}