using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using MasterDataFactory.Models.Domain.Machines;
using MasterDataFactory.Models.PersistenceContext;
using MasterDataFactory.Repositories;
using MasterDataFactory.Repositories.Impl;

namespace MasterDataFactory.Services
{
    public class MachineService
    {
        private readonly IMachineRepository _machineRepository;

        public MachineService(Context context)
        {
            _machineRepository = new MachineRepository(context);;
        }

        public async Task<ActionResult<IEnumerable<Machine>>> GetMachines()
        {
            return await _machineRepository.GetAll();
        }

        public async Task<ActionResult<Machine>> CreateMachine(Machine machine)
        {
            //TODO return
            await _machineRepository.Create(machine);
            return null;
        } 
    }
}