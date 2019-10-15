using Microsoft.EntityFrameworkCore;
using MasterDataFactory.Models.Domain.Operations;
using MasterDataFactory.Models.Domain.Machines;
using MasterDataFactory.Models.Domain.MachinesTypes;
using MasterDataFactory.Models.Domain.ProductionLines;
using MasterDataFactory.Models.Domain.MachineTypes;

namespace MasterDataFactory.Models.PersistenceContext
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        public DbSet<Machine> Machines { get; set; }
        public DbSet<MachineType> MachineTypes { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<ProductionLine> ProductionLines { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            /* E preciso usar isto quando utilizamos Value-Objects se esses VO nao fores chave
             modelBuilder.Entity<Machine>()
                .OwnsOne(p => p.MachineId);*/
            modelBuilder.Entity<Machine>()
                .Property(o => o.MachineId)
                .HasConversion(new MachineIdValueVConverter());
/*/
            modelBuilder.Entity<MachineType>()
            .HasIndex(b => new {b.Ref.id})
            .IsUnique();

            
                modelBuilder.Entity<MachineType>()
                .Property(o => o.Ref)
                .HasConversion(new MachineTypeIdValueVConverter());
            */
            modelBuilder.Entity<MachineType>(config =>
            {
            config.HasIndex(t => new { t.id, t.Ref })
            .IsUnique(true);

            config.Property(t => t.Ref)
            .HasConversion(new MachineTypeIdValueVConverter());
            
            });

            //modelBuilder.ApplyConfiguration(new MachineTypeConfig());
            //modelBuilder.ApplyConfiguration(new OrderItemConfig());
                
        }
    }
}