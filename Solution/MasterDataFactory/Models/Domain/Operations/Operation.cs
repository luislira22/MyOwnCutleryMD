
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

        public Operation(Guid id, string description)
        {
            this.Id = id;
            this.Description = new OperationDescription(description);
        }
    }
}
