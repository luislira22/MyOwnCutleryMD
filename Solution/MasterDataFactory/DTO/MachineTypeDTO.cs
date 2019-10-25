using System;
using System.Collections.Generic;
using MasterDataFactory.DTO.Operations;
using MasterDataFactory.Models.MachineTypes;

namespace MasterDataFactory.DTO
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