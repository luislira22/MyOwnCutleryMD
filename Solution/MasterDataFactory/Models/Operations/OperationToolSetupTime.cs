using System;
using System.Collections.Generic;

namespace MasterDataFactory.Models.Operations
{
    public class OperationToolSetupTime : ValueObject
    {
        public TimeSpan Value { get; set;}
        
        public OperationToolSetupTime(TimeSpan value)
        {
            Value = value;
        }

        public OperationToolSetupTime(string duration)
        {
            Validate(duration);
            Value = parseDuration(duration);
        }

        private void Validate(string duration)
        {
            if (duration == null)
                throw new ArgumentException("setup time is needed.");
        }
        
        private TimeSpan parseDuration(string duration)
        {
            if (TimeSpan.TryParse(duration,out TimeSpan result))
                return result;
            throw new FormatException("invalid setup time format.");
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }


        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != this.GetType())
                return false;
            OperationToolSetupTime operationToolSetupTime = (OperationToolSetupTime)obj;
            return Value.Equals(operationToolSetupTime.Value);
        }


        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}