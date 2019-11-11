using System;
using System.Collections.Generic;

namespace MasterDataFactory.Models.Operations
{
    public class OperationTool : ValueObject
    {
        public string Value { get; set;}
        
        public OperationTool(string value)
        {
            validate(value);
            Value = value;
        }

        public void validate(string value)
        {
            if(value == null)
                throw new ArgumentException("Tool is needed.");
            if(value.Equals(""))
                throw new ArgumentException("Tool can't be empty.");
        }
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
                return false;
            OperationTool operationTool = (OperationTool)obj;
            return Value.Equals(operationTool.Value);
        }
        
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}