using MasterDataFactory.Models.Domain.MachinesTypes;

namespace MasterDataFactory.Models.Domain.MachineTypes
{
    public class MachineTypeDTO
    {
        public int Ref {get;set;}
        public string Type {get;set;}

        public MachineTypeDTO(int Ref, string Type){
            this.Ref = Ref;
            this.Type = Type;
        }

    }
}