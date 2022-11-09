using AutoMapper;
using ImpactMeasurementAPI.DTOs;
using ImpactMeasurementAPI.Models;

namespace ImpactMeasurementAPI.Profiles
{
    public class ImpactProfile : Profile
    {
        public ImpactProfile()
        {
            //source -> target

            CreateMap<CreateTrainingSession, TrainingSession>()
                .ForMember(dest => dest.StartingTime,
                    opt => opt.MapFrom(src => src.StartingTime));
            CreateMap<TrainingSession, ReadTrainingSession>()
                .ForMember(dest => dest.StartingTime,
                    opt => opt.MapFrom(src => src.StartingTime))
                .ForMember(dest => dest.FreeAcceleration,
                    opt => opt.MapFrom(src => src.FreeAcceleration));
            CreateMap<CreateMomentarilyAcceleration, MomentarilyAcceleration>()
                .ForMember(dest => dest.Frame,
                    opt => opt.MapFrom(src => src.Frame))
                .ForMember(dest => dest.AccelerationX,
                        opt => opt.MapFrom(src => src.AccelerationX))
                .ForMember(dest => dest.AccelerationY,
                    opt => opt.MapFrom(src => src.AccelerationY))
                .ForMember(dest => dest.AccelerationZ,
                    opt => opt.MapFrom(src => src.AccelerationZ))
                .ForMember(dest => dest.TrainingSessionId,
                    opt => opt.MapFrom(src => src.TrainingSessionId));
            CreateMap<Impact, ReadImpact>()
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ImpactForce,
                    opt => opt.MapFrom(src => src.ImpactForce))
                .ForMember(dest => dest.ImpactDirectionX,
                    opt => opt.MapFrom(src => src.ImpactDirectionX))
                .ForMember(dest => dest.ImpactDirectionY,
                    opt => opt.MapFrom(src => src.ImpactDirectionY))
                .ForMember(dest => dest.ImpactDirectionZ,
                    opt => opt.MapFrom(src => src.ImpactDirectionZ));
        }
    }
}