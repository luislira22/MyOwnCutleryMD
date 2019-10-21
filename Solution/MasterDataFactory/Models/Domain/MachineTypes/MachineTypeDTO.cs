using System;
using System.Collections.Generic;
using MasterDataFactory.Models.Domain.MachinesTypes;
using MasterDataFactory.Models.Domain.Operations;

namespace MasterDataFactory.Models.Domain.MachineTypes
{
    public class MachineTypeDTO
    {
        public Guid Id { get; set; }
        public string Type { get; set; }

        public List<OperationDTO> Operations {get;set;}

        public MachineTypeDTO(Guid Id, string Type, List<OperationDTO> Ops)
        {
            this.Id = Id;
            this.Type = Type;
            this.Operations = Ops;
        }
    }
}