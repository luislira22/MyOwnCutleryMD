using MasterDataFactory.Models.ProductionLines;
using MasterDataFactory.Models.Operations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MasterDataFactory.Models.Machines;

namespace MasterDataFactory.Models.PersistenceContext
{
    public class ProductionLineConfiguration : IEntityTypeConfiguration<ProductionLine>
    {
        public ProductionLineConfiguration(){

        }

        public void Configure(EntityTypeBuilder<ProductionLine> ProductionLineConfiguration)
        {
            ProductionLineConfiguration.ToTable("ProductionLines", Context.DEFAULT_SCHEMA);
            ProductionLineConfiguration.HasKey(o => o.Id);
            ProductionLineConfiguration.OwnsOne(d => d.Description);
            ProductionLineConfiguration.HasMany<Machine>(o => o.Machines);
        }
    }   
}