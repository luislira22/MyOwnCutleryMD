using MasterDataFactory.Models.MachineTypesOperations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MasterDataFactory.Models.PersistenceContext
{
    public class MachineTypeOperationConfiguration : IEntityTypeConfiguration<MachineTypeOperation>
    {

        public MachineTypeOperationConfiguration()
        {

        }

        public void Configure(EntityTypeBuilder<MachineTypeOperation> MachineTypeConfiguration)
        {

            MachineTypeConfiguration.ToTable("MachineTypeOperations");

            MachineTypeConfiguration
            .HasKey(pt => new { pt.MachineTypeId, pt.OperationId });

            MachineTypeConfiguration
                .HasOne(pt => pt.MachineType)
                .WithMany(p => p.MachineTypeOperations)
                .HasForeignKey(pt => pt.MachineTypeId).OnDelete(DeleteBehavior.Restrict);

            MachineTypeConfiguration
                .HasOne(pt => pt.Operation)
                .WithMany(t => t.MachineTypeOperations)
                .HasForeignKey(pt => pt.OperationId).OnDelete(DeleteBehavior.Restrict);
            
        }
    }
}