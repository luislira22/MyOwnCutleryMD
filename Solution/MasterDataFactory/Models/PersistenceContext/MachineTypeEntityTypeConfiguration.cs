using MasterDataFactory.Models.MachineTypes;
using MasterDataFactory.Models.Operations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MasterDataFactory.Models.PersistenceContext
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
            MachineTypeConfiguration.HasMany<Operation>(o => o.Operations);//WithOne(m => m.Machine).IsRequired().HasForeignKey(m => m.MachineTypeId);
        }
    }
}