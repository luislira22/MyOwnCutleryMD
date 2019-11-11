using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MasterDataFactory.Models.Operations
{
    public class OperationDuration : ValueObject
    {
        public TimeSpan Value { get; set;}
        
        public OperationDuration(TimeSpan value)
        {
            Value = value;
        }

        public OperationDuration(string duration)
        {
            Validate(duration);
            Value = parseDuration(duration);
        }

        private void Validate(string duration)
        {
            if (duration == null)
                throw new ArgumentException("duration is needed.");
        }
        
        private TimeSpan parseDuration(string duration)
        {
            if (TimeSpan.TryParse(duration,out TimeSpan result))
                return result;
            throw new FormatException("invalid duration format.");
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }


        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != this.GetType())
                return false;
            OperationDuration operationDuration = (OperationDuration)obj;
            return Value.Equals(operationDuration.Value);
        }


        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}