using System.Collections.Generic;

namespace MasterDataFactory.Models.Machines
{
    public enum State
    {
        Activated,
        Deactivated
    }
    
    public class MachineState : ValueObject
    {
        
        public State State { get; private set;  }

        public MachineState(State state)
        {
            State = state;
        }
        
        protected MachineState()
        {
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return State;
        }
    }
}