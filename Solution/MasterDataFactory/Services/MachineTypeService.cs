using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MasterDataFactory.Models.Domain.MachineTypes;
using MasterDataFactory.Models.MachineTypes;
using MasterDataFactory.Models.PersistenceContext;
using MasterDataFactory.Repositories.Impl;
using MasterDataFactory.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MasterDataFactory.Services
{
    public class MachineTypeService
    {
        private readonly IMachineTypeRepository _machineTypeRepository;


        public MachineTypeService(Context context)
        {
            _machineTypeRepository = new MachineTypeRepository(context);
        }
        
        public async Task<bool> MachineExists(Guid id)
        {
            //TODO implementar reposotorios e deixar a linha de baixo
            //return await _machineRepository.Exists(id);
            return true;
        }
        public async Task<MachineType> getMachineType(Guid id)
        {
            try
            {
                var machineType = await _machineTypeRepository.GetById(id);
                return machineType;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task postMachineType(MachineType machine)
        {
            await _machineTypeRepository.Create(machine);
            await _machineTypeRepository.SaveChangesAsync();
        }

        public async Task<ActionResult<IEnumerable<MachineType>>> GetMachineTypes()
        {
            return await _machineTypeRepository.GetAll();
        }
    }
}