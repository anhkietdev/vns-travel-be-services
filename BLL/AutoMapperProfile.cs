using AutoMapper;
using BLL.DTOs;
using DAL.Models;

namespace BLL
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Service, ServiceDto>()
                .ForMember(dest => dest.ServiceId, opt => opt.MapFrom(src => src.ServiceId))
                .ForMember(dest => dest.PartnerId, opt => opt.MapFrom(src => src.PartnerId))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.ServiceType, opt => opt.MapFrom(src => src.ServiceType))
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location))
                .ReverseMap();

            CreateMap<Booking, BookingDto>()
                .ForMember(dest => dest.BookingType, opt => opt.MapFrom(src => src.BookingType))
                .ForMember(dest => dest.BookingStatus, opt => opt.MapFrom(src => src.BookingStatus))
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount))
                .ForMember(dest => dest.PaymentMethod, opt => opt.MapFrom(src => src.PaymentMethod))
                .ForMember(dest => dest.PaymentStatus, opt => opt.MapFrom(src => src.PaymentStatus))
                .ReverseMap();

            CreateMap<ServiceFilterDto, Service>()
                .ForMember(dest => dest.ServiceId, opt => opt.MapFrom(src => src.ServiceId))
                .ForMember(dest => dest.PartnerId, opt => opt.MapFrom(src => src.PartnerId))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.LocationId, opt => opt.MapFrom(src => src.LocationId))
                .ReverseMap();
        }
    }
}
