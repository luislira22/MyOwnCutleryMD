using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MasterDataFactory.DTO.Operations;
using MasterDataFactory.Models.MachineTypesOperations;

namespace MasterDataFactory.Models.Operations
{
    public class Operation : IEntity
    {
        public Guid Id { get; set; }

        public OperationDuration Duration { get; set; }

        public OperationDescription Description { get; set; }
        
        public OperationTool Tool { get; set; }

        public List<MachineTypeOperation> MachineTypeOperations { get; set; }
        
        protected Operation()
        {
            
        }

        public Operation(string description, TimeSpan duration, string tool)
        {
            Tool = new OperationTool(tool);
            Description = new OperationDescription(description);
            Duration = new OperationDuration(duration);
        }

        public Operation(Guid id, string description, TimeSpan duration,string tool)
        {
            Id = id;
            Description = new OperationDescription(description);
            Duration = new OperationDuration(duration);
            Tool = new OperationTool(tool);
        }

        public Operation(OperationDTO operationDto)
        {
            Duration = new OperationDuration(operationDto.Duration);
            Description = new OperationDescription(operationDto.Description);
            Tool = new OperationTool(operationDto.Tool);
        }


        public OperationDTO ToDTO()
        {
            return new OperationDTO(Id, Description.Value, Duration.Value.ToString(),Tool.Value);
        }
        
        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != this.GetType())
                return false;
            Operation operationTmp = (Operation)obj;
            return Id.Equals(operationTmp.Id) && 
                   Description.Equals(operationTmp.Description) && 
                   Duration.Equals(operationTmp.Duration) &&
                   Tool.Equals(operationTmp.Tool);
        }
        
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}