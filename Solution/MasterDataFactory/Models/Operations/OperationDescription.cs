using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        
        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != this.GetType())
                return false;
            OperationDescription operationDescription = (OperationDescription)obj;
            return Description.Equals(operationDescription.Description);
        }
        
        public override int GetHashCode()
        {
            return Description.GetHashCode();
        }
        
    }
}