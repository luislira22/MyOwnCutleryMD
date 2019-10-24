using System;
using System.Reflection.PortableExecutable;
using AutoMapper;

namespace MasterDataFactory.DTO
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //user to dto
            CreateMap<Machine, MachineDTO>()
                .ForMember(m => m.Id, dto => dto.MapFrom(u => Mapper.Map<Guid>(m.)) )
        }
    }
}