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
using Microsoft.AspNetCore.Mvc;

namespace MasterDataFactory.Services
{
    public class ProductionLineService
    {
        private readonly IProductionLineRepository _productionLineRepository;
        private readonly MachineService _machineService;

        public ProductionLineService(Context _context)
        {
            _productionLineRepository = new ProductionLineRepository(_context);
            _machineService = new MachineService(_context);
        }

        public ProductionLineService(Context _context, MachineService machineService)
        {
            _productionLineRepository = new ProductionLineRepository(_context);
            _machineService = machineService;
        }

        public async Task<ProductionLine> GetProductionLineById(Guid id)
        {
            ProductionLine ProductionLine = await _productionLineRepository.GetById(id);
            if (ProductionLine == null)
                throw new KeyNotFoundException();
            return ProductionLine;
        }

        public async Task<IEnumerable<ProductionLine>> GetProductionLines()
        {
            return await _productionLineRepository.GetAll();
        }
        public async Task<ProductionLine> PostProductionLine(ProductionLineDTO productionLineDTO)
        {
            List<Machine> machines = ValidateMachines(productionLineDTO.Machines).Result;
            ProductionLine productionLine = new ProductionLine(new ProductionLineDescription(productionLineDTO.description), machines);
            await _productionLineRepository.Create(productionLine);
            return productionLine;
        }

        public async Task DeleteProductionLineById(Guid id)
        {
            if (!_productionLineRepository.Exists(id).Result)
                throw new KeyNotFoundException();
            await _productionLineRepository.Delete(id);
        }

        private async Task<List<Machine>> ValidateMachines(List<string> MachinesId)
        {
            List<Machine> Machines = new List<Machine>();
            foreach (string strId in MachinesId)
            {
                if (!Guid.TryParse(strId, out Guid id))
                    throw new KeyNotFoundException(String.Format("id: {0} is not valid!", id));

                Machine Machine = await _machineService.GetMachineById(id);
                if (Machine == null)
                    throw new KeyNotFoundException(String.Format("The Machine with id: {0} was not found!", id));

                if (Machine.MachineState.State == State.Deactivated)
                {
                    throw new Exception("Machine is deactivated");
                }
                if (!Machines.Contains(Machine))
                    Machines.Add(Machine);
            }
            return Machines;
        }

        public async Task UpdateProductionLine(Guid Id, ProductionLineDTO productionLineDTO)
        {
            ProductionLine productionLine = await GetProductionLineById(Id);
            List<Machine> machines = ValidateMachines(productionLineDTO.Machines).Result;
            //Machine machine = await _machineService.GetMachineById(machineGuid);
            productionLine.Description = new ProductionLineDescription(productionLineDTO.description);
            productionLine.Machines = machines;
            await _productionLineRepository.Update(Id, productionLine);
        }
        
        public async Task RemoveMachineFromProductionLine(Guid machineId)
        {
            ProductionLine productionLine = await _productionLineRepository.GetProductionLineByMachine(machineId);
            List<Machine> newMachinesList = new List<Machine>();
            foreach (var machine in productionLine.Machines)
            {
                if (machine.Id != machineId) newMachinesList.Append(machine);
            }
            productionLine.Machines = newMachinesList;
            await _productionLineRepository.Update(productionLine.Id, productionLine);
        }

    }
}