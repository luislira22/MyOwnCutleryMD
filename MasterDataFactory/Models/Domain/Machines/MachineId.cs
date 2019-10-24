using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MasterDataFactory.Models.Domain.Machines
{
    public class MachineId
    {
        public string Reference { get; }

        public MachineId(string referencia)
        {
            this.Reference = referencia;
        }
    }

    public class MachineIdValueVConverter : ValueConverter<MachineId, string>
    {
        public MachineIdValueVConverter(ConverterMappingHints mappingHints = null) 
            : base (
                id => id.Reference,
                reference => new MachineId(reference),
                mappingHints
                ){}
    }
    

}
