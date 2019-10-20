using System;
using System.ComponentModel.DataAnnotations;
using MasterDataFactory.Models.Domain.MachineTypes;

namespace MasterDataFactory.Models.Domain.Machines
{
    public class Machine
    {
        public Guid Id { get; set; }
        [Required]
        public MachineType MachineType { get; set; }

        public Machine(MachineType machineType)
        {
            MachineType = machineType;
        }

        public MachineDTO toDTO()
        {
            return new MachineDTO(MachineType);
        }
    }
}
