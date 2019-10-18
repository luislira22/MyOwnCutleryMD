using System.Collections.Generic;

namespace MasterDataFactory.Models.Domain.Operations
{
    public class OperationDescription : ValueObject
    {
        public string Description { get; set; }

        public OperationDescription()
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