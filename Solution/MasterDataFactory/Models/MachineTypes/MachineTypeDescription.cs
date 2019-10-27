using System.Collections.Generic;

namespace MasterDataFactory.Models.MachineTypes
{
    public class MachineTypeDescription : ValueObject
    {
        public string Type { get; set; }

        public MachineTypeDescription(string type){
            Type = type;
        }

        protected MachineTypeDescription()
        {
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Type;
        }
    }
}