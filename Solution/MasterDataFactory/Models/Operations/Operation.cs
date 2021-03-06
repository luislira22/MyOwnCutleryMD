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

        public virtual OperationDuration Duration { get; set; }

        public virtual OperationDescription Description { get; set; }
        
        public virtual OperationTool Tool { get; set; }

        public virtual OperationToolSetupTime SetupTime { get; set; }

        public virtual List<MachineTypeOperation> MachineTypeOperations { get; set; }
        
        protected Operation()
        {
            
        }

        public Operation(string description, TimeSpan duration, string tool,TimeSpan setupTime)
        {
            Tool = new OperationTool(tool);
            Description = new OperationDescription(description);
            Duration = new OperationDuration(duration);
            SetupTime = new OperationToolSetupTime(setupTime);
        }

        public Operation(Guid id, string description, TimeSpan duration,string tool,TimeSpan setupTime)
        {
            Id = id;
            Description = new OperationDescription(description);
            Duration = new OperationDuration(duration);
            Tool = new OperationTool(tool);
            SetupTime = new OperationToolSetupTime(setupTime);
        }

        public Operation(OperationDTO operationDto)
        {
            Duration = new OperationDuration(operationDto.Duration);
            Description = new OperationDescription(operationDto.Description);
            Tool = new OperationTool(operationDto.Tool);
            SetupTime = new OperationToolSetupTime(operationDto.SetupTime);
        }


        public OperationDTO ToDTO()
        {
            return new OperationDTO(Id, Description.Value, Duration.Value.ToString(),Tool.Value,SetupTime.Value.ToString());
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