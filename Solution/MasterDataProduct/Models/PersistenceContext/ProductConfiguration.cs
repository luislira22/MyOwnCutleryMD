using MasterDataProduct.Models.Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MasterDataProduct.Models.PersistenceContext
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> productConfiguration)
        {
            productConfiguration.ToTable("Product");
            productConfiguration.HasKey(o => o.Id);
            productConfiguration.OwnsOne(o => o.Plan);
            productConfiguration.Property(o => o.Plan).IsRequired();
        }
    }
}