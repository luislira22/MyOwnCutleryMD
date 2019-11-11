using System;

namespace MasterDataFactory.DTO.Operations
{
    public class OperationDTO
    {
        public Guid Id;
        public string Duration { get; set; }
        
        public string Description{get;set;}
        
        public string Tool { get; set; }

        public OperationDTO(Guid id,string description,string duration,string tool)
        {
            Id = id;
            Description = description;
            Duration = duration;
            Tool = tool;
        }
    }
}