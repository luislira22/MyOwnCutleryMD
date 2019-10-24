
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using MasterDataFactory.Models.Domain.MachineTypes;
using Newtonsoft.Json;

namespace MasterDataFactory.Models.Domain.Operations
{
    public class Operation
    {
        public Guid Id{ get; set; }
        
        public OperationDescription Description{ get;set;}


        //public Guid? MachineTypeId { get; set; }

        //[JsonIgnore] 
        //[IgnoreDataMember] //prevenir ciclo infinito
        //public MachineType Machine { get; set; }

        public Operation(){
            
        }

        public Operation(string description)
        {
            this.Description = new OperationDescription(description);
        }

        public OperationDTO toDTO(){
            return new OperationDTO(Id,Description.Description);
        }
    }
}
