using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using MasterDataFactory.Models.Machines;
using MasterDataFactory.Models.PersistenceContext;
using MasterDataFactory.Repositories.Impl;
using MasterDataFactory.Repositories.Interfaces;

namespace MasterDataFactory.Services
{
    public class MachineService
    {
        private readonly IMachineRepository _machineRepository;

        public MachineService(Context context)
        {
            _machineRepository = new MachineRepository(context);
        }

        public async Task<bool> MachineExists(Guid id)
        {
            return await _machineRepository.Exists(id);
        }

        public async Task<ActionResult<List<Machine>>> GetMachines()
        {
            return await _machineRepository.GetAll();
        }

        public async Task CreateMachine(Machine machine)
        {
            await _machineRepository.Create(machine);
        }

        public async Task DeleteMachine(Guid id)
        {
            await _machineRepository.Delete(id);
        }

        public async Task<List<Machine>> GetMachineByType(Guid type)
        {
            return await _machineRepository.GetByType(type);
            
        }
    }
}