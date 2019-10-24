using System;
using System.Collections.Generic;

namespace MasterDataFactory.Models.Domain.Operations
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
            return TimeSpan.Parse(duration);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Duration;
        }


        public override bool Equals(object obj)
        {
            return Duration.Equals(obj);
        }
    }
}