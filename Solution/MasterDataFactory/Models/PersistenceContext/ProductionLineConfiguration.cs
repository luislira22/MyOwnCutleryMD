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

        public void Configure(EntityTypeBuilder<ProductionLine> productionLineConfiguration)
        {
            productionLineConfiguration.ToTable("ProductionLines", Context.DEFAULT_SCHEMA);
            productionLineConfiguration.HasKey(o => o.Id);
            productionLineConfiguration.OwnsOne(d => d.Description);
            productionLineConfiguration.HasMany<Machine>(o => o.Machines);
        }
    }   
}