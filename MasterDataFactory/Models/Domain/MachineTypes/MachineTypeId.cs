using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MasterDataFactory.Models.Domain.MachinesTypes
{
    public class MachineTypeID
    {
        public string id { get; set; }

        public MachineTypeID(string id) {
            this.id = id;
        }
    }

    
    public class MachineTypeIdValueVConverter : ValueConverter<MachineTypeID, string>
    {
        public MachineTypeIdValueVConverter(ConverterMappingHints mappingHints = null) 
            : base (
                id => id.id,
                reference => new MachineTypeID(reference),
                mappingHints
                ){}
    }
}
