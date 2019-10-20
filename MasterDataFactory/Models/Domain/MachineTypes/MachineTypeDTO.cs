using System;
using MasterDataFactory.Models.Domain.MachinesTypes;

namespace MasterDataFactory.Models.Domain.MachineTypes
{
    public class MachineTypeDTO
    {
        public Guid Id { get; set; }
        public string Type { get; set; }

        public MachineTypeDTO(string Type)
        {
            this.Type = Type;
        }
    }
}