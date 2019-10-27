using System;
using MasterDataFactory.DTO.Machines;
using MasterDataFactory.Models;
using MasterDataFactory.Models.MachineTypes;

namespace MasterDataFactory.Models.Machines
{
    public class Machine : IEntity
    {
        public Guid Id { get; set; }
        public MachineType MachineType { get; set; }
        public MachineBrand MachineBrand { get; set; }
        public MachineModel MachineModel { get; set; }
        public MachineLocation MachineLocation { get; set; }

        public Machine(MachineType machineType, MachineBrand machineBrand, MachineModel machineModel,
            MachineLocation machineLocation)
        {
            MachineType = machineType;
            MachineBrand = machineBrand;
            MachineModel = machineModel;
            MachineLocation = machineLocation;
        }

        public Machine()
        {
        }

        public MachineDTO toDTO()
        {
            return null; //acho q era fixe fazer isto guilherme
            //_mapper.Map<Machine, MachineDTO>(this);
        }
    }
}