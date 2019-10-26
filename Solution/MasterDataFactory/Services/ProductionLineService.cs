using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterDataFactory.DTO.ProductionLines;
using MasterDataFactory.Models.Machines;
using MasterDataFactory.Models.PersistenceContext;
using MasterDataFactory.Models.ProductionLines;
using MasterDataFactory.Repositories.Impl;
using MasterDataFactory.Repositories.Interfaces;

namespace MasterDataFactory.Services
{
    public class ProductionLineService
    {
        private readonly IProductionLineRepository _productionLineRepository;
        private readonly MachineService _machineService;

        public ProductionLineService(Context _context){
           _productionLineRepository = new ProductionLineRepository(_context);
           _machineService = new MachineService(_context);
        }

        public async Task<ProductionLine> GetProductionLineById(Guid id){
            ProductionLine ProductionLine =  await _productionLineRepository.GetById(id);
            if(ProductionLine == null)
                throw new KeyNotFoundException();
            return ProductionLine;
        }
        public async Task<ProductionLine> PostProductionLine(ProductionLineDTO productionLineDTO)
        {
            List<Machine> machines = ValidateMachines(productionLineDTO.Machines).Result;
            ProductionLine productionLine = new ProductionLine(machines);
            await _productionLineRepository.Create(productionLine);
            return productionLine;
        }

        public async Task DeleteProductionLineById(Guid id)
        {
            if(!_productionLineRepository.Exists(id).Result)
                throw new KeyNotFoundException();
            await _productionLineRepository.Delete(id);
        }

        private async Task<List<Machine>> ValidateMachines(List<string> MachinesId)
        {
            List<Machine> Machines = new List<Machine>();
            foreach (string strId in MachinesId)
            {
                if (!Guid.TryParse(strId,out Guid id))
                    throw new KeyNotFoundException(String.Format("id: {0} is not valid!", id));
                
                Machine Machine = await _machineService.GetMachineById(id);
                if(Machine == null)
                    throw new KeyNotFoundException(String.Format("The Machine with id: {0} was not found!", id));
                if(!Machines.Contains(Machine))
                    Machines.Add(Machine);
            }
            return Machines;
        }

    }
}