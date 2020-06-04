using AutoMapper;
using FluentValidationApp.Web.DTOs;
using FluentValidationApp.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluentValidationApp.Web.Mapping
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<EventDateDto, EventDate>()
                .ForMember(x => x.Date, opt => opt.MapFrom(x => new DateTime(x.Year, x.Month, x.Day)));

            CreateMap<EventDate, EventDateDto>()
                .ForMember(d => d.Year, opt => opt.MapFrom(m => m.Date.Year))
                .ForMember(d => d.Month, opt => opt.MapFrom(m => m.Date.Month))
                .ForMember(d => d.Day, opt => opt.MapFrom(m => m.Date.Day));

           
        }
        EventProfile eventProfile = new EventProfile();

        
    }
}
