using System;
using MasterDataFactory.DTO.Operations;

namespace MasterDataFactory.Models.Operations
{
    public class Operation : IEntity
    {
        public Guid Id { get; set; }
        public OperationDuration Duration { get; set; }
        public OperationDescription Description { get; set; }


        //public Guid? MachineTypeId { get; set; }

        //[JsonIgnore] 
        //[IgnoreDataMember] //prevenir ciclo infinito
        //public MachineType Machine { get; set; }

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


        public OperationDTO toDTO()
        {
            return new OperationDTO(Id, Description.Description, Duration.Duration.ToString());
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != this.GetType())
                return false;
            Operation OperationTmp = ((Operation) obj);
            return OperationTmp.Description.Equals(this.Description) || Duration.Equals(OperationTmp.Duration);
        }


        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}