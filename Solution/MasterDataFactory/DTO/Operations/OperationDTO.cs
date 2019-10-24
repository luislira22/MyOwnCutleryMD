using System;

namespace MasterDataFactory.Models.Domain.Operations
{
    public class OperationDTO
    {
        public Guid Id;
        public string Duration { get; set; }
        
        public string Description{get;set;}

        public OperationDTO(Guid id,string description,string duration)
        {
            this.Id = id;
            this.Description = description;
            this.Duration = duration;
        }
    }
}