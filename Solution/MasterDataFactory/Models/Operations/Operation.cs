using System;
using System.Collections.Generic;
using MasterDataFactory.DTO.Operations;
using MasterDataFactory.Models.MachineTypesOperations;

namespace MasterDataFactory.Models.Operations
{
    public class Operation : IEntity
    {
        public Guid Id { get; set; }
        
        public OperationDuration Duration { get; set; }
        public OperationDescription Description { get; set; }
        public List<MachineTypeOperation> MachineTypeOperations { get; set; }
        
        protected Operation()
        {
        }

        public Operation(string description, TimeSpan duration)
        {
            Description = new OperationDescription(description);
            Duration = new OperationDuration(duration);
        }

        public Operation(Guid id, string description, TimeSpan duration)
        {
            Id = id;
            Description = new OperationDescription(description);
            Duration = new OperationDuration(duration);
        }

        public Operation(OperationDTO operationDto)
        {
            Duration = new OperationDuration(operationDto.Duration);
            Description = new OperationDescription(operationDto.Description);
        }


        public OperationDTO ToDTO()
        {
            return new OperationDTO(Id, Description.Description, Duration.Duration.ToString());
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != this.GetType())
                return false;
            Operation operationTmp = ((Operation) obj);
            return Id.Equals(operationTmp.Id);
        }


        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}