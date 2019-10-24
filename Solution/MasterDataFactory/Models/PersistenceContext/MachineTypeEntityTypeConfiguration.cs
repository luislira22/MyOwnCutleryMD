using MasterDataFactory.Models.PersistenceContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MasterDataFactory.Models.Domain.MachineTypes
{
    public class MachineTypeEntityTypeConfiguration : IEntityTypeConfiguration<MachineType>
    {
        public MachineTypeEntityTypeConfiguration(){

        }

        public void Configure(EntityTypeBuilder<MachineType> MachineTypeConfiguration)
        {
            MachineTypeConfiguration.ToTable("MachineTypes", Context.DEFAULT_SCHEMA);
            MachineTypeConfiguration.HasKey(o => o.Id);
            MachineTypeConfiguration.OwnsOne(o => o.Type);
            MachineTypeConfiguration.OwnsMany(o => o.Operations);
        }
    }
}