using MasterDataFactory.Models.Machines;

namespace MasterDataFactory.DTO.Machines
{
    public class MachineDTO
    {
        public string Id { get; set; }
        public string MachineType { get; set; }
        public string MachineBrand { get; set; }
        public string MachineModel { get; set; }
        public string MachineLocation { get; set; }
        public string MachineState { get; set; }

        public MachineDTO()
        {
        }
    }
}