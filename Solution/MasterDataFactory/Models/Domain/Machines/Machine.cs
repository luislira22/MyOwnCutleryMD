using System;
using System.ComponentModel.DataAnnotations;
using MasterDataFactory.Models.Domain.MachineTypes;

namespace MasterDataFactory.Models.Domain.Machines
{
    public class Machine : IEntity
    {
        public Guid Id { get; set; }
        [Required] public MachineType MachineType { get; set; }

        public Machine(MachineType machineType)
        {
            MachineType = machineType;
        }

        protected Machine()
        {
        }

        public MachineDTO toDTO()
        {
            return new MachineDTO(MachineType);
        }
    }
}