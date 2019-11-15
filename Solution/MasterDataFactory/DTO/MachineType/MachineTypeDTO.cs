using System;
using System.Collections.Generic;
using MasterDataFactory.DTO.Operations;
using MasterDataFactory.Models.MachineTypes;

namespace MasterDataFactory.DTO
{
    public class MachineTypeDTO
    {
        public string Id { get; set; }
        public string Type { get; set; }

        public List<string> Operations {get;set;}

        public MachineTypeDTO(string Id, string Type, List<string> Ops)
        {
            this.Id = Id;
            this.Type = Type;
            this.Operations = Ops;
        }
    }
}