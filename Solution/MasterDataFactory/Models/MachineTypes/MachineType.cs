using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterDataFactory.Models.Domain.MachinesTypes;
using MasterDataFactory.Models.Domain.Operations;
using MasterDataFactory.Models.MachineTypes;

namespace MasterDataFactory.Models.Domain.MachineTypes
{
    public class MachineType : IEntity
    {
        public Guid Id {get;set;}

        public MachineTypeDescription Type { get; set; }

        public List<Operation> Operations {get;set;}

        protected MachineType()
        {

        }

        public MachineType(MachineTypeDescription Type, List<Operation> ops)
        {
            this.Type = Type;
            this.Operations = ops;
        }

        public MachineTypeDTO toDTO()
        {
            List<OperationDTO> dtoOps = new List<OperationDTO>();
            foreach(Operation op in Operations){
                dtoOps.Add(op.toDTO());
            }

            return new MachineTypeDTO(Id,Type,dtoOps);
        }
    }
}
