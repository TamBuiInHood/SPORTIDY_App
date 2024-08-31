﻿using AutoMapper;
using FSU.SPORTIDY.Repository.Entities;
using FSU.SPORTIDY.Service.BusinessModel.ClubModels;
using FSU.SPORTIDY.Service.BusinessModel.MeetingModels;
using FSU.SPORTIDY.Service.BusinessModel.PlayFieldsModels;
using FSU.SPORTIDY.Service.BusinessModel.SportBsModels;
using FSU.SPORTIDY.Service.BusinessModel.UserModels;
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

            CreateMap<Meeting, MeetingModel>()
                .ForMember(dto => dto.CommentInMeetings, opt => opt.MapFrom(entity => entity.CommentInMeetings))
                .ForMember(dto => dto.UserMeetings, opt => opt.MapFrom(entity => entity.UserMeetings))
                .ReverseMap()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<User, UserModel>()
                .ForMember(x => x.RoleName, opt => opt.MapFrom(x => x.Role.RoleName))
                .ReverseMap();
            CreateMap<Sport, SportDTO>()
                .ForMember(dto => dto.Users, opt => opt.MapFrom(entity => entity.Users))
                .ReverseMap();

            CreateMap<Club, CreateClubModel>().ReverseMap();

            CreateMap<UserClub, UserModel>()
                .ForMember(x => x.UserId, opt => opt.MapFrom(x => x.User.UserId))
                .ForMember(x => x.UserCode, opt => opt.MapFrom(x => x.User.UserCode))
                .ForMember(x => x.UserName, opt => opt.MapFrom(x => x.User.UserName))
                .ForMember(x => x.Address, opt => opt.MapFrom(x => x.User.Address))
                .ForMember(x => x.FullName, opt => opt.MapFrom(x => x.User.FullName))
                .ForMember(x => x.RoleName, opt => opt.MapFrom(x => x.User.Role.RoleName))
                .ForMember(x => x.Avartar, opt => opt.MapFrom(x => x.User.Avartar))
                .ForMember(x => x.CreateDate, opt => opt.MapFrom(x => x.User.CreateDate))
                .ForMember(x => x.Gender, opt => opt.MapFrom(x => x.User.Gender))
                .ForMember(x => x.Description, opt => opt.MapFrom(x => x.User.Description))
                .ForMember(x => x.IsDeleted, opt => opt.MapFrom(x => x.User.IsDeleted))
                .ForMember(x => x.Birtday, opt => opt.MapFrom(x => x.User.Birtday))
                .ForMember(x => x.Status, opt => opt.MapFrom(x => x.User.Status))
                .ForMember(x => x.Phone, opt => opt.MapFrom(x => x.User.Phone))
                .ForMember(x => x.Email, opt => opt.MapFrom(x => x.User.Email))
                .ReverseMap();

            CreateMap<Club, ClubModel>()
                    .ForMember(dest => dest.ListMember, opt => opt.MapFrom(src => src.UserClubs.Select(cm => cm.User)))
                    .ReverseMap();

            CreateMap<UserClub, ClubModel>()
                 .ForMember(x => x.ClubId, opt => opt.MapFrom(x => x.ClubId))
                .ForMember(x => x.ClubCode, opt => opt.MapFrom(x => x.Club.ClubCode))
                .ForMember(x => x.ClubName, opt => opt.MapFrom(x => x.Club.ClubName))
                .ForMember(x => x.Regulation, opt => opt.MapFrom(x => x.Club.Regulation))
                .ForMember(x => x.Infomation, opt => opt.MapFrom(x => x.Club.Infomation))
                .ForMember(x => x.Slogan, opt => opt.MapFrom(x => x.Club.Slogan))
                .ForMember(x => x.MainSport, opt => opt.MapFrom(x => x.Club.MainSport))
                .ForMember(x => x.CreateDate, opt => opt.MapFrom(x => x.Club.CreateDate))
                .ForMember(x => x.Location, opt => opt.MapFrom(x => x.Club.Location))
                .ForMember(x => x.TotalMember, opt => opt.MapFrom(x => x.Club.TotalMember))
                .ForMember(x => x.AvartarClub, opt => opt.MapFrom(x => x.Club.AvartarClub))
                .ForMember(x => x.CoverImageClub, opt => opt.MapFrom(x => x.Club.CoverImageClub))
                 .ForMember(x => x.ListMember, opt => opt.MapFrom(x => x.Club.UserClubs.Select(uc => uc.User)))
                .ReverseMap();

            CreateMap<PlayField, PlayFieldModel>()
                .ForMember(dest => dest.Bookings, opt => opt.MapFrom(src => src.Bookings))
                .ForMember(dest => dest.ImageFields, opt => opt.MapFrom(src => src.ImageFields))
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                .ReverseMap();
        }
    }
}
