using MasterDataFactory.Models.Domain.MachinesTypes;

namespace MasterDataFactory.Models.Domain.MachineTypes
{
    public class MachineTypeDTO
    {
        public string Type {get;set;}

        public MachineTypeDTO(string Type){
            this.Type = Type;
        }

    }
}