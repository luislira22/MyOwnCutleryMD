using Microsoft.EntityFrameworkCore;
using Project.Models.Domain.MasterDataFactory.Machines;

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