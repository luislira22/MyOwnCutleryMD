using MasterDataFactory.Models.MachineTypes;
using MasterDataFactory.Models.Operations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MasterDataFactory.Models.PersistenceContext
{
    public class MachineTypeConfiguration : IEntityTypeConfiguration<MachineType>
    {
        public MachineTypeConfiguration(){

        }

        public void Configure(EntityTypeBuilder<MachineType> MachineTypeConfiguration)
        {
            MachineTypeConfiguration.ToTable("MachineTypes");
            MachineTypeConfiguration.HasKey(o => o.Id);
            MachineTypeConfiguration.OwnsOne(o => o.Type).Property(t => t.Type).HasColumnName("Tipo");
        }
    }   
}