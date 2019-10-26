using System;
using MasterDataFactory.Models.MachineTypes;
using MasterDataFactory.Models.Operations;

namespace MasterDataFactory.Models.MachineTypesOperations
{
    public class MachineTypeOperation
    {
        public Guid MachineTypeId { get; set; }
        public MachineType MachineType { get; set; }
        public Guid OperationId { get; set; }
        public Operation Operation { get; set; }

        public MachineTypeOperation(MachineType MachineType, Operation Operation)
        {
            this.MachineType = MachineType;
            this.Operation = Operation;
        }

        protected MachineTypeOperation(){

        }
    }
}