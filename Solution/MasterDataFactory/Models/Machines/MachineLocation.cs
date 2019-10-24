using System.Collections.Generic;

namespace MasterDataFactory.Models.Machines
{
    public class MachineLocation : ValueObject
    {
        public string Location { get; private set; }

        public MachineLocation(string location)
        {
            Location = location;
        }

        protected MachineLocation()
        {
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Location;
        }
    }
}