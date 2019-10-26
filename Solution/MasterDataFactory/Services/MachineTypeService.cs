using System;
using System.Collections.Generic;
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
        
        public async Task<ICollection<Operation>> getOperations(Guid id)
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

        public async Task<MachineType> postMachineType(MachineTypeDTO machinTypeDTO)
        {
            ICollection<Operation> operations = ValidateOperations(machinTypeDTO.Operations).Result;
            MachineType machineType = new MachineType(new MachineTypeDescription(machinTypeDTO.Type), operations);
            await _machineTypeRepository.Create(machineType);
            await _machineTypeRepository.SaveChangesAsync();
            return machineType;
        }

        public async Task<ActionResult<IEnumerable<MachineType>>> GetMachineTypes()
        {
            return await _machineTypeRepository.GetAll();
        }

        public async Task UpdateMachineTypeOperation(Guid id,ICollection<string> operationsId)
        {
            MachineType machineType = getMachineType(id).Result;
            machineType.Operations = ValidateOperations(operationsId).Result;
            await _machineTypeRepository.Update(id,machineType);
        }

        private async Task<ICollection<Operation>> ValidateOperations(ICollection<string> operationsId)
        {
            ICollection<Operation> operations = new List<Operation>();
            foreach (string strId in operationsId)
            {
                
                if (!Guid.TryParse(strId,out Guid id))
                    throw new KeyNotFoundException(String.Format("id: {0} is not valid!", id));
                
                Operation operation = await _serviceOperations.GetOperationById(id);
                if(operation == null)
                    throw new KeyNotFoundException(String.Format("The operation with id: {0} was not found!", id));
                if(!operations.Contains(operation))
                    operations.Add(operation);
            }
            return operations;
        }
    }
}