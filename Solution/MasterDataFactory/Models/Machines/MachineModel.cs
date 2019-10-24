using System.Collections.Generic;

namespace MasterDataFactory.Models.Machines
{
    public class MachineModel : ValueObject
    {
        public string Model { get; private set;  }

        public MachineModel(string model)
        {
            Model = model;
        }
        
        protected MachineModel()
        {
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Model;
        }
    }
}