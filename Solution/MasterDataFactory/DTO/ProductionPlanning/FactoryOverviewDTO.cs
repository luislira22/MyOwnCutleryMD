using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MasterDataFactory.DTO.ProductionPlanning
{
    public class FactoryOverviewDTO
    {
        //public IEnumerable ProductionLines { get; set; }
        //public IEnumerable Machines { get; set; }
        //public IEnumerable Tools { get; set; }
        public IEnumerable<string> ProductionLines;
        public IEnumerable<ProductionLineMachinesDTO> ProductionLineMachines { get;set; }
        public IEnumerable<MachineOperationDTO> OperationMachines { get; set; }
    }
}