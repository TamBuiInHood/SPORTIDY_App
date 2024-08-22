using AutoMapper;
using FSU.SPORTIDY.Repository.Entities;
using FSU.SPORTIDY.Service.Models.MeetingModels;
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

            CreateMap<Meeting, MeetingDTO>()
                .ForMember(dto => dto.CommentInMeetings, opt => opt.MapFrom(entity => entity.CommentInMeetings))
                .ForMember(dto => dto.UserMeetings, opt => opt.MapFrom(entity => entity.UserMeetings))
                .ReverseMap()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        }
    }
}
