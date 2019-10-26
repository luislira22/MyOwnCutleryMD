using System.Collections.Generic;

namespace MasterDataFactory.Models.Operations
{
    public class OperationDescription : ValueObject
    {
        public string Description { get; set; }

        protected OperationDescription()
        {
            
        }
        
        public OperationDescription(string description)
        {
            this.Description = description;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Description;
        }
        
    }
}