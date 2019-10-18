using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace MasterDataFactory.Models.Domain.Machines
{
    public class MachineId : ValueObject
    {
        public Guid Id { get; private set; }
        
        private MachineId(Guid id)
        {
            Id = id;
        }

        /*public static implicit operator long(MachineId beaconId)
        {
            return beaconId.Id;
        }

        public static implicit operator MachineId(int id)
        {
            return new MachineId(id);
        }*/

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Id;
            //throw new System.NotImplementedException();
        }
    }
}
