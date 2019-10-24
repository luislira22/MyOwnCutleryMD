using MasterDataFactory.Models.Machines;

namespace MasterDataFactory.DTO
{
    public class MachineDTO
    {
        public string Id { get; set; }
        public string MachineType { get; set; }
        public string MachineBrand { get; set; }
        public string MachineModel { get; set; }
        public string MachineLocation { get; set; }

        public MachineDTO(Machine machine)
        {
            Id = machine.Id.ToString();
            if (machine.MachineType != null)
            {
                MachineType = machine.MachineType.Id.ToString();
            }

            MachineBrand = machine.MachineBrand.Brand;
            MachineModel = machine.MachineModel.Model;
            MachineLocation = machine.MachineLocation.Location;
        }

        public MachineDTO()
        {
        }
    }
}