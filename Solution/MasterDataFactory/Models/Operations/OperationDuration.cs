using System;
using System.Collections.Generic;

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
            return Duration.Equals(obj);
        }


        public override int GetHashCode()
        {
            return Duration.GetHashCode();
        }
    }
}