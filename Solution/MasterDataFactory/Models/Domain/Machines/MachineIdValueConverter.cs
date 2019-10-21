using System;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MasterDataFactory.Models.Domain.Machines
{
    public class MachineIdValueConverter : ValueConverter<MachineId, Guid>
    {
        public MachineIdValueConverter(ConverterMappingHints mappingHints = null)
            : base(
                id => id.referencia,
                referencia => new MachineId(referencia),
                mappingHints
            ) { }
    }
}