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

            int productionLineCounter = 0;
            int machineCounter = 0;
            
            FactoryOverviewDTO factoryOverviewDto = new FactoryOverviewDTO();
           
            IEnumerable productionLines = await _productionLineService.GetProductionLines();
            
            //DTO belongings
            List<string> lines = new List<string>();
            List<ProductionLineMachinesDTO> plmDtoL = new List<ProductionLineMachinesDTO>();
            List<MachineOperationDTO> moDtoL = new List<MachineOperationDTO>();
            
            foreach (ProductionLine productionLine in productionLines)
            {
                List<string> machines = new List<string>();
                ProductionLineMachinesDTO plmDTO = new ProductionLineMachinesDTO();
                lines.Add($"l{productionLineCounter}");
                plmDTO.ProductionLine = $"l{productionLineCounter}";
                productionLineCounter++;
                foreach (Machine machine in productionLine.Machines)
                {
                    machines.Add($"m{machineCounter}");
                    foreach ( MachineTypeOperation machineTypeOperation in machine.MachineType.MachineTypeOperations)
                    {
                        MachineOperationDTO moDTO = new MachineOperationDTO();
                        Operation operation = machineTypeOperation.Operation;
                        
                        moDTO.OperationType = operation.Id.ToString();
                        moDTO.Machine = $"m{machineCounter}";
                        moDTO.Tool = operation.Tool.Value;
                        moDTO.ExecutionTime= operation.Duration.Value.TotalSeconds;
                        moDTO.SetupTime = operation.SetupTime.Value.TotalSeconds;

                        moDtoL.Add(moDTO);
                    }
                    machineCounter++;
                }
                plmDTO.Machines = machines;
                plmDtoL.Add(plmDTO);
            }
            factoryOverviewDto.ProductionLines = lines;
            factoryOverviewDto.OperationMachines = moDtoL;
            factoryOverviewDto.ProductionLineMachines = plmDtoL;
            return factoryOverviewDto;
        }
    }
}