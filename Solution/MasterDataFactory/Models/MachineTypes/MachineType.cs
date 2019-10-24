using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterDataFactory.Models.Domain.MachinesTypes;
using MasterDataFactory.Models.Domain.Operations;

namespace MasterDataFactory.Models.MachineTypes
{
    public class MachineType : IEntity
    {
        public Guid Id {get;set;}

        //Não há suporte para implementar em .NET Core uma chave primária que é um valueobject
        //public MachineTypeID Id {get;set;}
        public ICollection<Machine> Machines { get; set; }

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
