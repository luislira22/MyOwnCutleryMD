
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDataFactory.Models.Domain.Operations
{
    public class Operation
    {
        
        //public OperationId Id{ get; set; }
        public Guid Id{ get; set; }
        
        public OperationDescription Description{ get;set;}

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
