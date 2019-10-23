using System.Collections.Generic;

namespace MasterDataFactory.Models.Machines
{
    public class MachineBrand : ValueObject
    {
        public string Brand { get; private set; }

        public MachineBrand(string brand)
        {
            Brand = brand;
        }

        protected MachineBrand()
        {
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Brand;
        }
    }
}