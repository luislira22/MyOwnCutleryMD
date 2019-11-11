using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;


namespace MasterDataFactory.Models.Operations
{
    public class OperationDescription : ValueObject
    {
        public string Value { get; set; }

        public OperationDescription(string value)
        {
            Validate(value);
            Value = value;
        }

        private void Validate(string value)
        {
            if(value == null)
                throw new ArgumentException("Description is needed.");
            if(value.Equals(""))
                throw new ArgumentException("Description can't be empty.");
                
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
        
        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != this.GetType())
                return false;
            OperationDescription operationDescription = (OperationDescription)obj;
            return Value.Equals(operationDescription.Value);
        }
        
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
        
    }
}