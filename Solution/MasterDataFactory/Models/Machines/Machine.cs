using System;
using MasterDataFactory.DTO.Machines;
using MasterDataFactory.Models;
using MasterDataFactory.Models.MachineTypes;

namespace MasterDataFactory.Models.Machines
{
    public class Machine : IEntity
    {
        public Guid Id { get; set; }
        public virtual MachineType MachineType { get; set; }
        public virtual MachineBrand MachineBrand { get; set; }
        public virtual MachineModel MachineModel { get; set; }
        public virtual MachineLocation MachineLocation { get; set; }
        public virtual MachineState MachineState { get; set; }

        public Machine(MachineType machineType, MachineBrand machineBrand, MachineModel machineModel,
            MachineLocation machineLocation)
        {
            MachineType = machineType;
            MachineBrand = machineBrand;
            MachineModel = machineModel;
            MachineLocation = machineLocation;
            MachineState = new MachineState(State.Activated);
        }

        public Machine()
        {
        }

        public bool deactivateMachine()
        {
            this.MachineState = new MachineState(State.Deactivated);
            return true;
        }
        
        public bool activateMachine()
        {
            this.MachineState = new MachineState(State.Activated);
            return true;
        }
    }
}