using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MasterDataFactory.DTO;
using MasterDataFactory.DTO.Operations;
using MasterDataFactory.Models.Machines;
using MasterDataFactory.Models.Operations;
using System.Linq;
using System.Threading.Tasks;

namespace MasterDataFactory.Models.MachineTypes
{
    public class MachineType : IEntity
    {
        public Guid Id {get;set;}

        //Não há suporte para implementar em .NET Core uma chave primária que é um valueobject
        //public MachineTypeID Id {get;set;}
        //public ICollection<Machine> Machines { get; set; }

        public MachineTypeDescription Type { get; set; }

        public ICollection<Operation> Operations {get;set;}

        protected MachineType()
        {

        }

        public MachineType(MachineTypeDescription type, ICollection<Operation> ops)
        {
            Type = type;
            Operations = ops;
        }

        public MachineTypeDTO toDTO()
        {
            List<string> dtoOps = (Operations.Select(op => op.Id.ToString())).ToList();
            return new MachineTypeDTO(Id.ToString(),Type.Type,dtoOps);
        }
    }
}
