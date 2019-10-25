using System;
using AutoMapper;
using MasterDataFactory.DTO.Machines;
using MasterDataFactory.Models.Machines;

namespace MasterDataFactory.DTO
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Machine, MachineDTO>()
                .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.MachineType, act =>
                {
                    act.PreCondition(src => (src.MachineType != null));
                    act.MapFrom(src => src.MachineType.Id.ToString());
                })
                .ForMember(dest => dest.MachineBrand, act => act.MapFrom(src => src.MachineBrand.Brand))
                .ForMember(dest => dest.MachineModel, act => act.MapFrom(src => src.MachineModel.Model))
                .ForMember(dest => dest.MachineLocation, act => act.MapFrom(src => src.MachineLocation.Location));
        }
    }
}