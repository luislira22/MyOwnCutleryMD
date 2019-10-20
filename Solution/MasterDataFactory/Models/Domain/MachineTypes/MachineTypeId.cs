using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MasterDataFactory.Models.Domain.MachinesTypes
{
    public class MachineTypeID : ValueObject
    {
        public int id { get; set; }

        public MachineTypeID(int id) {
            this.id = id;
        }

        public MachineTypeID()
        {
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return id;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            
            // TODO: write your implementation of Equals() here
            MachineTypeID other = (MachineTypeID)obj;
            return other.id == this.id;
        }
        
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    /*
    public class MachineTypeIdValueVConverter : ValueConverter<MachineTypeID, string>
    {
        public MachineTypeIdValueVConverter(ConverterMappingHints mappingHints = null) 
            : base (
                id => id.id,
                reference => new MachineTypeID(reference),
                mappingHints
                ){}
    }
    */
}
