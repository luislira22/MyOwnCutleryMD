using MasterDataFactory.Models.Machines;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MasterDataFactory.Models.PersistenceContext
{
    public class MachineConfiguration : IEntityTypeConfiguration<Machine>
    {
        public void Configure(EntityTypeBuilder<Machine> machineConfiguration)
        {
            machineConfiguration.ToTable("machine");
            machineConfiguration.HasKey(o => o.Id);
            machineConfiguration.HasOne(o => o.MachineType);
                //.WithMany(p => p.Machines).IsRequired(); não é ligação bidirecional guilherme - @tomas
            machineConfiguration.OwnsOne(o => o.MachineBrand);
            machineConfiguration.OwnsOne(o => o.MachineModel);
            machineConfiguration.OwnsOne(o => o.MachineLocation);
            machineConfiguration.OwnsOne(o => o.MachineState);
        }
    }
}