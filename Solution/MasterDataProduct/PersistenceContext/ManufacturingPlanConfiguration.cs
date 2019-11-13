using MasterDataProduct.Models.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MasterDataProduct.PersistenceContext
{
    public class ManufacturingPlanConfiguration : IEntityTypeConfiguration<ManufacturingPlan>
    {
        public void Configure(EntityTypeBuilder<ManufacturingPlan> manufacturingPlanConfiguration)
        {
            manufacturingPlanConfiguration.ToTable("ManufacturingPlan");
            manufacturingPlanConfiguration.HasKey(mp => mp.Id);
            manufacturingPlanConfiguration.HasMany(mp => mp.Ids);
            //.WithOne(oi => oi.ManufacturingPlan);
        }
    }
}