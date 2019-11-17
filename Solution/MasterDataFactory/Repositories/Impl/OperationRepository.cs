using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterDataFactory.Models.MachineTypes;
using MasterDataFactory.Models.MachineTypesOperations;
using MasterDataFactory.Models.Operations;
using MasterDataFactory.Models.PersistenceContext;
using MasterDataFactory.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MasterDataFactory.Repositories.Impl
{
    public class OperationRepository : GenericRepository<Operation>, IOperationRepository
    {

        public OperationRepository(Context context) : base(context)
        {
            //context.Operations.Include(m => m.MachineTypeOperations).ToListAsync();
        }

        public async override Task<bool> Exists(Guid id)
        {
            return await _context.Operations.AnyAsync(o => o.Id == id);
        }

        public async Task DeleteWithRelationship(Guid operationId)
        {
            //Apagar Operation + MachineTypeOperation(join row) + MachineType
            Operation operation = GetById(operationId).Result;
            MachineTypeRepository machineTypeRepository = new MachineTypeRepository(_context);
            MachineTypeOperationRepository jointablerepo = new MachineTypeOperationRepository(_context);
            List<MachineType> machineTypes = await jointablerepo.GetMachineTypesWithOperationId(operation.Id);
            foreach (MachineType machine in machineTypes)
            {
                List<MachineTypeOperation> joinsList = machine.MachineTypeOperations.ToList();
                foreach (MachineTypeOperation join in joinsList)
                    if (join.Operation.Equals(operation))
                    {
                        machine.MachineTypeOperations.Remove(join); //apagar da lista
                        await jointablerepo.Delete(join.MachineTypeId, join.OperationId); //apagar
                        if (machine.MachineTypeOperations.Count == 0)
                        {
                            //0-* Se MachineType n tiver nenhum operation n existe e é apagada
                            await machineTypeRepository.Delete(machine.Id);
                        }
                    }
            }
            await base.Delete(operationId); //apagar Operation
        }
    }
}