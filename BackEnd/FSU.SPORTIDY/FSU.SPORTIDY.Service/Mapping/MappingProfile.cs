using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FSU.SPORTIDY.Service.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mapping classes
            
            //CreateMap<Menu, MenuDTO>()
            //    .ForMember(dto => dto.BrandName, opt => opt.MapFrom(entity => entity.Brand.BrandName))
            //    .ForMember(dto => dto.MenuLists, opt => opt.MapFrom(entity => entity.MenuLists))
            //    .ForMember(dto => dto.MenuSegments, opt => opt.MapFrom(entity => entity.MenuSegments))
            //    .ReverseMap();
            
        }
    }
}
