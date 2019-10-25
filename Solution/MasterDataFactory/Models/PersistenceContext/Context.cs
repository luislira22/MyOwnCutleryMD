using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using MasterDataFactory.Models.Domain.Operations;
using MasterDataFactory.Models.Domain.ProductionLines;
using MasterDataFactory.Models.Domain.MachineTypes;
using MasterDataFactory.Models.Machines;
using MasterDataFactory.Models.MachineTypes;
using MasterDataFactory.Models.Operations;

namespace MasterDataFactory.Models.PersistenceContext
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
            
        }

        protected Context()
        {
        }
        
        public static string DEFAULT_SCHEMA { get; internal set; }
        public virtual DbSet<Machine> Machines { get; set; }
        public virtual DbSet<MachineType> MachineTypes { get; set; }
        public virtual DbSet<Operation> Operations { get; set; }
        public  virtual DbSet<ProductionLine> ProductionLines { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MachineTypeEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new MachineConfiguration());
            modelBuilder.ApplyConfiguration(new OperationConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}