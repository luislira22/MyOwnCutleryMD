using Microsoft.EntityFrameworkCore;
using MasterDataFactory.Models.Domain.Operations;
using MasterDataFactory.Models.Domain.Machines;
using MasterDataFactory.Models.Domain.MachinesTypes;
using MasterDataFactory.Models.Domain.ProductionLines;

namespace MasterDataFactory.Models.PersistenceContext
{
    public class Context : DbContext
    {       
         public Context(DbContextOptions<MachineContext> options): base(options)
        {

        }

        public DbSet<Machine> Machines { get; set; }
        public DbSet<MachineType> MachineTypes { get; set; }
        public DbSet<Operation> operationDBSet { get; set; }
        public DbSet<ProductionLine> ProductionLines { get; set; }
    }


}