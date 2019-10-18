using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using MasterDataFactory.Models.Domain.Operations;
using MasterDataFactory.Models.Domain.Machines;
using MasterDataFactory.Models.Domain.ProductionLines;
using MasterDataFactory.Models.Domain.MachineTypes;

namespace MasterDataFactory.Models.PersistenceContext
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        public static string DEFAULT_SCHEMA { get; internal set; }
        public DbSet<Machine> Machines { get; set; }
        public DbSet<MachineType> MachineTypes { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<ProductionLine> ProductionLines { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            

            modelBuilder.ApplyConfiguration(new MachineConfiguration());
            modelBuilder.ApplyConfiguration(new MachineTypeEntityTypeConfiguration());
            base.OnModelCreating(modelBuilder);

            /* E preciso usar isto quando utilizamos Value-Objects se esses VO nao fores chave
                                                          modelBuilder.Entity<Machine>()
                                                               .OwnsOne(p => p.MachineId);*/
            /*modelBuilder.Entity<Machine>()
                .Property(o => o.MachineId)
                .HasConversion(new MachineIdValueVConverter());*/

            /*modelBuilder.Entity<Machine>(
                config =>
                {
                    config.ToTable("machine");
                    config.HasKey(o => o.MachineId);
                    //config.OwnsOne(o => o.MachineId);
                });*/

        }
    }
}