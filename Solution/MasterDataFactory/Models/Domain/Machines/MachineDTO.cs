using MasterDataFactory.Models.Domain.MachineTypes;

namespace MasterDataFactory.Models.Domain.Machines
{
    public class MachineDTO
    {
        public MachineTypeDTO MachineType { get; }

        public MachineDTO(MachineType machineType)
        {
            MachineType = machineType.toDTO();
        }
    }
}