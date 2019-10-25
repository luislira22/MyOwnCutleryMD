using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using MasterDataFactory.DTO.Machines;
using MasterDataFactory.Models.Machines;
using MasterDataFactory.Models.PersistenceContext;
using MasterDataFactory.Repositories.Impl;
using MasterDataFactory.Repositories.Interfaces;

namespace MasterDataFactory.Services
{
    public class MachineService
    {
        private readonly IMachineRepository _machineRepository;
        private readonly IMachineTypeRepository _machineTypeRepository;

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

        public async Task<Machine> CreateMachine(MachineDTO machineDTO)
        {
            var machineTypeId = Guid.Parse(machineDTO.MachineType);
            var machineType = await _machineTypeRepository.GetById(machineTypeId);
            if (machineType == null) throw new KeyNotFoundException();

            var machineBrand = new MachineBrand(machineDTO.MachineBrand);
            var machineModel = new MachineModel(machineDTO.MachineModel);
            var machineLocation = new MachineLocation(machineDTO.MachineLocation);

            var machine = new Machine(machineType, machineBrand, machineModel, machineLocation);
            await _machineRepository.Create(machine);
            return machine;
        }

        public async Task DeleteMachine(Guid id)
        {
            var machine = await _machineRepository.GetById(id);
            if (machine == null) throw new KeyNotFoundException();
            
            await _machineRepository.Delete(id);
        }

        public async Task<List<Machine>> GetMachineByType(Guid type)
        {
            return await _machineRepository.GetByType(type);
            
        }
    }
}