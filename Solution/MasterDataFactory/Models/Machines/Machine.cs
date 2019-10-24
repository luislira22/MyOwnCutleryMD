using System;
using MasterDataFactory.DTO;
using MasterDataFactory.Models.Domain;
using MasterDataFactory.Models.Domain.MachineTypes;
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


        public Machine(MachineType machineType, MachineBrand machineBrand, MachineModel machineModel, MachineLocation machineLocation)
        {
            MachineType = machineType;
            MachineBrand = machineBrand;
            MachineModel = machineModel;
            MachineLocation = machineLocation;
        }

        protected Machine()
        {
        }

        public MachineDTO toDTO()
        {
            return new MachineDTO(this);
        }
    }
}