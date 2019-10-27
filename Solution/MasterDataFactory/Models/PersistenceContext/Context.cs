using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using MasterDataFactory.Models.Operations;
using MasterDataFactory.Models.ProductionLines;
using MasterDataFactory.Models.MachineTypes;
using MasterDataFactory.Models.Machines;
using MasterDataFactory.Models.MachineTypesOperations;

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
        public virtual DbSet<ProductionLine> ProductionLines { get; set; }
        public virtual DbSet<MachineTypeOperation> MachineTypeOperations { get; set; } //join table

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MachineTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ProductionLineConfiguration());
            modelBuilder.ApplyConfiguration(new MachineConfiguration());
            modelBuilder.ApplyConfiguration(new OperationConfiguration());

            modelBuilder.Entity<MachineTypeOperation>()
            .HasKey(pt => new { pt.MachineTypeId, pt.OperationId });

            modelBuilder.Entity<MachineTypeOperation>()
                .HasOne(pt => pt.MachineType)
                .WithMany(p => p.MachineTypeOperations)
                .HasForeignKey(pt => pt.MachineTypeId);

            modelBuilder.Entity<MachineTypeOperation>()
                .HasOne(pt => pt.Operation)
                .WithMany(t => t.MachineTypeOperations)
                .HasForeignKey(pt => pt.OperationId).OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}