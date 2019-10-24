using System;
using System.Collections.Generic;
using MasterDataFactory.Models.Domain.Operations;

namespace MasterDataFactory.Models.MachineTypes
{
    public class MachineTypeDTO
    {
        public Guid Id { get; set; }
        public string Type { get; set; }

        public List<OperationDTO> Operations {get;set;}

        public MachineTypeDTO(Guid Id, MachineTypeDescription Type, List<OperationDTO> Ops)
        {
            this.Id = Id;
            this.Type = Type.Type;
            this.Operations = Ops;
        }
    }
}