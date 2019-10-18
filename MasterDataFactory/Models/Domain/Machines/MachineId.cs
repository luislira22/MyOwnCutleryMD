using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MasterDataFactory.Models.Domain.Machines
{
    public class MachineId : ValueObject
    {
        public long Id { get; private set; }
        
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Id;
            //throw new System.NotImplementedException();
        }
    }
}
