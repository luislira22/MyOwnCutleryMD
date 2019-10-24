using System.Reflection.Emit;
using MasterDataFactory.Models.Domain.Machines;
using Microsoft.EntityFrameworkCore;

namespace MasterDataFactory.Models.PersistenceContext
{
    public class MachineContext : DbContext
    {
        public DbSet<Machine> Machines { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            /* E preciso usar isto quando utilizamos Value-Objects se esses VO nao fores chave
             modelBuilder.Entity<Machine>()
                .OwnsOne(p => p.MachineId);*/
<<<<<<< Updated upstream:Solution/MasterDataFactory/Models/PersistenceContext/MachineContext.cs
            
            modelBuilder.Entity<Machine>()
                .Property(o => o.MachineId)
                .HasConversion(new MachineIdValueVConverter());
            
=======
            modelBuilder.Entity<Machine>()
                .Property(o => o.MachineId)
                .HasConversion(new MachineIdValueVConverter());
>>>>>>> Stashed changes:MasterDataFactory/Models/PersistenceContext/MachineContext.cs
        }

        public MachineContext(DbContextOptions<MachineContext> options) : base(options) { }




    }
}