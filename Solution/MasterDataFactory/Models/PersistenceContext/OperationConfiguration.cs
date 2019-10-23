using MasterDataFactory.Models.Domain.Operations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MasterDataFactory.Models.PersistenceContext
{
    public class OperationConfiguration : IEntityTypeConfiguration<Operation>
    {
        public void Configure(EntityTypeBuilder<Operation> operationConfiguration)
        {
            operationConfiguration.ToTable("operations", Context.DEFAULT_SCHEMA);
            operationConfiguration.HasKey(o => o.Id);
            operationConfiguration.OwnsOne(d => d.Description, d =>
            {
                d.Property(p => p.Description).HasMaxLength(300)
                    .HasColumnName("Description")
                    .IsRequired();
            });
        }
    }
}