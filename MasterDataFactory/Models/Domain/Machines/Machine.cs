using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterDataFactory.Models.Domain.MachinesTypes;

namespace MasterDataFactory.Models.Domain.Machines
{
    public class Machine
    {
        public MachineId MachineId { get; set; }
        public string Nome { get; set; }
        
    }
}
