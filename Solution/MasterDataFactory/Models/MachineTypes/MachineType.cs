using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterDataFactory.Models.Domain.MachinesTypes;
using MasterDataFactory.Models.Domain.Operations;

namespace MasterDataFactory.Models.Domain.MachineTypes
{
    public class MachineType : IEntity
    {
        //internal MachineTypeID Ref { get; private set; } //identificacao no dominio (value object) ex: PCX010
        public Guid Id {get;set;}

        public string Type { get; set; }

        public List<Operation> Operations {get;set;}

        public MachineType()
        {

        }

        public MachineType(string Type, List<Operation> ops)
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
