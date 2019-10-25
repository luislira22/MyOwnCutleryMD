using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterDataFactory.DTO;
using MasterDataFactory.Models.MachineTypes;
using MasterDataFactory.Models.Operations;
using MasterDataFactory.Models.PersistenceContext;
using MasterDataFactory.Repositories.Impl;
using MasterDataFactory.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MasterDataFactory.Services
{
    public class MachineTypeService
    {
        private readonly IMachineTypeRepository _machineTypeRepository;
        private readonly OperationService _serviceOperations;


        public MachineTypeService(Context context)
        {
            _machineTypeRepository = new MachineTypeRepository(context);
            _serviceOperations = new OperationService(context);
        }
        
        public async Task<List<Operation>> getOperations(Guid id)
        {
            var machineType = await _machineTypeRepository.GetById(id);
            if (machineType == null) throw new KeyNotFoundException();

            return machineType.Operations;
        }
        public async Task<MachineType> getMachineType(Guid id)
        {
            MachineType machineType = await _machineTypeRepository.GetById(id);
            if (machineType == null)
            {
                throw new KeyNotFoundException();
            }
            return machineType;
        }

        public async Task<MachineType> postMachineType(MachineTypeDTO machine)
        {
            List<Operation> operations = new List<Operation>();
            foreach (string opDTO in machine.Operations)
            {
                Operation op = await _serviceOperations.getOperationById(Guid.Parse(opDTO));
                if (op == null)
                {
                    throw new KeyNotFoundException(String.Format("The operation with id: {0} was not found!", opDTO));
                }
                operations.Add(op);
            }
            MachineType machineType = new MachineType(new MachineTypeDescription(machine.Type), operations);
            await _machineTypeRepository.Create(machineType);
            await _machineTypeRepository.SaveChangesAsync();
            return machineType;
        }

        public async Task<ActionResult<IEnumerable<MachineType>>> GetMachineTypes()
        {
            return await _machineTypeRepository.GetAll();
        }
    }
}