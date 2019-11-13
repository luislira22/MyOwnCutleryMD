using MasterDataProduct.Models.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MasterDataProduct.PersistenceContext
{
    public class OperationIdConfiguration : IEntityTypeConfiguration<OperationId>
    {
        public void Configure(EntityTypeBuilder<OperationId> operationIdConfiguration)
        {
            operationIdConfiguration.ToTable("OperationId");
            operationIdConfiguration.HasKey(oi => oi.Id);
            operationIdConfiguration.Property(oi => oi.Value);
            operationIdConfiguration.Property(oi => oi.Index);
            //operationIdConfiguration.HasOne(oi => oi.ManufacturingPlan);
            //.WithMany(mp => mp.Ids);
        }
    }
}