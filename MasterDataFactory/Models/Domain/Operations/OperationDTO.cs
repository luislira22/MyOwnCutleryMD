using System;

namespace MasterDataFactory.Models.Domain.Operations
{
    public class OperationDTO
    {
        public Guid Id {get;set;} 
        
        public string Description{get;set;}

        public OperationDTO(Guid id,string description)
        {
            this.Id = id;
            this.Description = description;
        }
    }
}