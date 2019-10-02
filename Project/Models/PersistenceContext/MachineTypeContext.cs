using Microsoft.EntityFrameworkCore;
using Project.Models.Domain.MasterDataFactory.MachineTypes;

namespace Project.Models.PersistenceContext
{
    public class MachineTypeContext : DbContext
    {
        public MachineTypeContext(DbContextOptions<MachineTypeContext> options) : base(options)
        {

        }

        public DbSet<MachineType> MachineTypes { get; set; }
    }
}