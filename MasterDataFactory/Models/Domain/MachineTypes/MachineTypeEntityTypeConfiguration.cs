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
            MachineTypeConfiguration.HasKey(o => o.Type);
            //MachineTypeConfiguration.Property(o => o.Ref).ForSqlServerUseSequenceHiLo("machineseq", Context.DEFAULT_SCHEMA);

        //MachineTypeConfiguration.OwnsOne(o => o.Ref);
            //MachineTypeConfiguration.Ignore(b => b.DomainEvents);
            //MachineTypeConfiguration.Property(o => o.Id)
                //.ForSqlServerUseSequenceHiLo("MachineTypeseq", MachineTypeingContext.DEFAULT_SCHEMA);


        }
    }
}