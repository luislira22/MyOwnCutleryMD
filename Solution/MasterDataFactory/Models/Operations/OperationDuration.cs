using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MasterDataFactory.Models.Operations
{
    public class OperationDuration : ValueObject
    {
        public TimeSpan Duration { get; set;}
        
        public OperationDuration(TimeSpan duration)
        {
            Duration = duration;
        }

        public OperationDuration(string duration)
        {
            Duration = parseDuration(duration);
        }
        
        private TimeSpan parseDuration(string duration)
        {
            if (TimeSpan.TryParse(duration,out TimeSpan result))
                return result;
            throw new FormatException("invalid duration format.");
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Duration;
        }


        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != this.GetType())
                return false;
            OperationDuration operationDuration = (OperationDuration)obj;
            return Duration.Equals(operationDuration.Duration);
        }


        public override int GetHashCode()
        {
            return Duration.GetHashCode();
        }
    }
}