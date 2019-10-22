using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MasterDataFactory.Models.Domain.Machines
{
    public class MachineConfiguration : IEntityTypeConfiguration<Machine>
    {
        public void Configure(EntityTypeBuilder<Machine> machineConfiguration)
        {
            machineConfiguration.ToTable("machine");
            machineConfiguration.HasKey(o => o.Id);
            machineConfiguration.OwnsOne(o => o.MachineModel);
            machineConfiguration.HasOne(o => o.MachineType);
        }
    }
}