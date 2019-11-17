using System;
using System.ComponentModel.DataAnnotations;
using MasterDataFactory.Models.MachineTypes;
using MasterDataFactory.Models.Operations;

namespace MasterDataFactory.Models.MachineTypesOperations
{
    public class MachineTypeOperation
    {
        [Key]
        public Guid MachineTypeId { get; set; }
        public virtual MachineType MachineType { get; set; }
        
        [Key]
        public Guid OperationId { get; set; }
        public virtual Operation Operation { get; set; }

        public MachineTypeOperation(MachineType MachineType, Operation Operation)
        {
            this.MachineTypeId = MachineType.Id;
            this.OperationId = Operation.Id;
            this.MachineType = MachineType;
            this.Operation = Operation;
        }

        protected MachineTypeOperation(){

        }
    }
}