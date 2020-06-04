using AutoMapper;
using FluentValidationApp.Web.DTOs;
using FluentValidationApp.Web.Models;

namespace FluentValidationApp.Web.Mapping
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerDto>().ReverseMap();

            CreateMap<Customer, MusteriDto>()
                .ForMember(dest => dest.Isim, source => source.MapFrom(x => x.Name))
                .ForMember(dest => dest.Eposta, source => source.MapFrom(x => x.Email))
                .ForMember(dest => dest.Yas, source => source.MapFrom(x => x.Age));
        }
    }
}