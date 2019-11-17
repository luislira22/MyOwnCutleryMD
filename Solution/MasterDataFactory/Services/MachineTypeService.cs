using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MasterDataFactory.DTO;

using MasterDataFactory.Models.MachineTypes;
using MasterDataFactory.Models.MachineTypesOperations;
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
        private readonly IMachineTypeOperationRepository _machineTypesOperationsRepository;

        private readonly OperationService _serviceOperations;


        public MachineTypeService(Context context)
        {
            _machineTypeRepository = new MachineTypeRepository(context);
            _machineTypesOperationsRepository = new MachineTypeOperationRepository(context);
            _serviceOperations = new OperationService(context);
        }
        
        public async Task<ICollection<Operation>> getOperations(Guid id)
        {
            var machineType = await _machineTypeRepository.GetById(id);
            if (machineType == null) throw new KeyNotFoundException();
            return await _machineTypesOperationsRepository.GetOperationsByMachineType(id);
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
            IList<Operation> operations = ValidateOperations(machinTypeDTO.Operations).Result;
            MachineType machineType = new MachineType(new MachineTypeDescription(machinTypeDTO.Type), operations);
            await _machineTypeRepository.Create(machineType);
            return machineType;
        }

        public async Task Delete(Guid id){
            await _machineTypeRepository.Delete(id);
        }

        public async Task<ActionResult<IEnumerable<MachineType>>> GetMachineTypes()
        {
            return await _machineTypeRepository.GetAll();
        }

        public async Task UpdateMachineTypeOperation(Guid id,IList<string> operationsId)
        {
            MachineType machineType = getMachineType(id).Result;

            ICollection<Operation> operations = ValidateOperations(operationsId).Result;
            List<MachineTypeOperation> machineTypeOperations = new List<MachineTypeOperation>();
            foreach(Operation op in operations){
                MachineTypeOperation machineTypeOperation = new MachineTypeOperation(machineType, op);
                machineTypeOperations.Add(machineTypeOperation);
            }
            machineType.MachineTypeOperations = machineTypeOperations;

            await _machineTypeRepository.UpdateWithRelationship(id,machineType);
        }

        private async Task<IList<Operation>> ValidateOperations(IList<string> operationsId)
        {
            IList<Operation> operations = new List<Operation>();
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