using System.ComponentModel.DataAnnotations.Schema;
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
        }
    }
}