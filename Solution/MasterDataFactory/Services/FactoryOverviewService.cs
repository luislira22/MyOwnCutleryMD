using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterDataFactory.DTO.ProductionPlanning;
using MasterDataFactory.Models.Machines;
using MasterDataFactory.Models.MachineTypesOperations;
using MasterDataFactory.Models.Operations;
using MasterDataFactory.Models.PersistenceContext;
using MasterDataFactory.Models.ProductionLines;

namespace MasterDataFactory.Services
{
    public class FactoryOverviewService
    {
        //private readonly MachineService _machineService;
        //private readonly OperationService _operationService;
        private readonly ProductionLineService _productionLineService;
        
        public FactoryOverviewService(Context context)
        {
            _productionLineService = new ProductionLineService(context);
        }
        public async Task<FactoryOverviewDTO> GetFactoryOverview()
        {
            FactoryOverviewDTO factoryOverviewDto = new FactoryOverviewDTO();
            
            //IEnumerable machines = await _machineService.GetMachines();
            //IEnumerable operations = await _operationService.GetOperations();
            IEnumerable productionLines = await _productionLineService.GetProductionLines();

            List<ProductionLineMachinesDTO> plmDtoL = new List<ProductionLineMachinesDTO>();
            List<MachineOperationDTO> moDtoL = new List<MachineOperationDTO>();
            
            foreach (ProductionLine productionLine in productionLines)
            {
                List<string> machines = new List<string>();
                ProductionLineMachinesDTO plmDTO = new ProductionLineMachinesDTO();
                plmDTO.ProductionLine = productionLine.Id.ToString();
                foreach (Machine machine in productionLine.Machines)
                {
                    machines.Add(machine.Id.ToString());
                    foreach ( MachineTypeOperation machineTypeOperation in machine.MachineType.MachineTypeOperations)
                    {
                        MachineOperationDTO moDTO = new MachineOperationDTO();
                        Operation operation = machineTypeOperation.Operation;
                        
                        moDTO.OperationType = operation.Description.Value;
                        moDTO.Machine = machine.Id.ToString();
                        moDTO.Tool = operation.Tool.Value;
                        moDTO.ExecutionTime= operation.Duration.Value.TotalSeconds;
                        moDTO.SetupTime = operation.SetupTime.Value.TotalSeconds;

                        moDtoL.Add(moDTO);
                    }
                }
                plmDTO.Machines = machines;
                plmDtoL.Add(plmDTO);
            }

            factoryOverviewDto.OperationMachines = moDtoL;
            factoryOverviewDto.ProductionLineMachines = plmDtoL;
            return factoryOverviewDto;
        }
    }
}