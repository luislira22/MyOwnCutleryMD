using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MasterDataFactory.Models.Domain.MachinesTypes
{
    public class MachineTypeID : ValueObject
    {
        public Guid Id { get; set; }

        public MachineTypeID(Guid id) {
            this.Id = id;
        }

        public MachineTypeID()
        {
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Id;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            
            // TODO: write your implementation of Equals() here
            MachineTypeID other = (MachineTypeID)obj;
            return other.Id == this.Id;
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
