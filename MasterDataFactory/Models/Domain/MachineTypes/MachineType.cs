using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterDataFactory.Models.Domain.MachinesTypes;

namespace MasterDataFactory.Models.Domain.MachineTypes
{
    public class MachineType
    {
        public long id { get; set; } //id da base de dados

        public MachineTypeID Ref { get; set; } //identificacao no dominio (value object) ex: PCX010
        public string Type { get; set; }

        public MachineType()
        {

        }

        public MachineType(string Ref, string Type)
        {
            this.Ref = new MachineTypeID(Ref);
            this.Type = Type;
        }

        public MachineTypeDTO toDTO()
        {
            return new MachineTypeDTO(Ref.id, Type);
        }
    }
}
