using System;
using System.Collections.Generic;
using MasterDataFactory.Models.PersistenceContext;
using System.Threading.Tasks;
using MasterDataFactory.Models.Operations;
using MasterDataFactory.Repositories.Impl;
using MasterDataFactory.Repositories.Interfaces;

namespace MasterDataFactory.Services
{
    public class OperationService
    {
        private readonly IOperationRepository _operationRepository;
        public OperationService(Context _context){
           _operationRepository = new OperationRepository(_context);
        }

        public async Task<Boolean> OperationExists(Guid id)
        {
            return await _operationRepository.Exists(id);
        }
        public async Task<List<Operation>> GetOperations()
        {
            return await _operationRepository.GetAll();
        }

        public async Task<Operation> GetOperationById(Guid id){
            Operation operation =  await _operationRepository.GetById(id);
            if(operation == null)
                throw new KeyNotFoundException();
            return operation;
        }
        public async Task PostOperation(Operation operation)
        {
            await _operationRepository.Create(operation);
        }

        public async Task DeleteOperationById(Guid id)
        {
            //Operation Operation = await GetOperationById(id);
            if(!_operationRepository.Exists(id).Result)
                throw new KeyNotFoundException();
            await _operationRepository.DeleteWithRelationship(id);
        }
    }
}