using System;
using MasterDataFactory.Models.Domain.MachineTypes;

namespace MasterDataFactory.Models.Domain.Machines
{
    public class Machine
    {
        public Guid Id { get; set; }
        public MachineType MachineType { get; set; }
    }
}
