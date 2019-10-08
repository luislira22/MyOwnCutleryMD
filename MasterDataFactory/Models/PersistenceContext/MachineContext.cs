using Microsoft.EntityFrameworkCore;
using MasterDataFactory.Models.Domain.Machines;

namespace Project.Models.PersistenceContext
{
    public class MachineContext : DbContext
    {
        public MachineContext(DbContextOptions<MachineContext> options)
            : base(options)
        {
        }

        public DbSet<Machine> Machines { get; set; }
    }
}