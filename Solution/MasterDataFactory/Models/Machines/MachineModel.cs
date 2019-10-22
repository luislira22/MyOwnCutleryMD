using System.Collections.Generic;

namespace MasterDataFactory.Models.Domain.Machines
{
    public class MachineModel : ValueObject
    {
        public string Model { get; set; }

        protected MachineModel()
        {
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Model;
        }
    }
}