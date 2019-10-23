
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MasterDataFactory.Models.Domain.Operations
{
    public class Operation
    {
        public Guid Id{ get; set;}
        
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
