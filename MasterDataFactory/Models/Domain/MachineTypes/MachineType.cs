﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterDataFactory.Models.Domain.MachinesTypes;

namespace MasterDataFactory.Models.Domain.MachineTypes
{
    public class MachineType : IEntity
    {
        //internal MachineTypeID Ref { get; private set; } //identificacao no dominio (value object) ex: PCX010

        public Guid Id {get;set;}

        public string Type { get; set; }

        public MachineType()
        {

        }

        public MachineType(string Type)
        {
            //this.Ref = new MachineTypeID(Ref);
            this.Type = Type;
        }

        public MachineTypeDTO toDTO()
        {
            return new MachineTypeDTO(Type);
        }
    }
}
