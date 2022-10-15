using AutoMapper;
using ImpactMeasurementAPI.DTOs;
using ImpactMeasurementAPI.Models;

namespace ImpactMeasurementAPI.Profiles
{
    public class ImpactProfile : Profile
    {
        public ImpactProfile()
        {
            
            //source->target
            //
            // CreateMap<TrainingSession, ReadAccelerationOnAxes>()
            //     .ForMember(dest => dest.FreeAccelerationX,
            //         opt => opt.MapFrom(src => src.FreeAccelerationX.Values))
            //     .ForMember(dest => dest.FreeAccelerationY,
            //     opt => opt.MapFrom(src => src.FreeAccelerationY.Values))
            //     .ForMember(dest => dest.FreeAccelerationZ,
            //     opt => opt.MapFrom(src => src.FreeAccelerationZ.Values));
        }
    }
}