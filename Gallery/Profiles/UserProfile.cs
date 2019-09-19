using AutoMapper;
using BLL.DTO;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Gallery.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>()
                .ForMember(dto =>  dto.Email, cfg => cfg.MapFrom(u => u.Email))
                .ForMember(dto => dto.UserName, cfg => cfg.MapFrom(u => u.UserName))
                .ForMember(dto => dto.PasswordHash, cfg => cfg.MapFrom(u => u.PasswordHash))
                .ForMember(dto => dto.Id, cfg => cfg.MapFrom(u => u.Id));
            CreateMap<UserDTO, User>()
                .ForMember(u => u.Email, cfg => cfg.MapFrom(dto => dto.Email))
                .ForMember(u => u.UserName, cfg => cfg.MapFrom(dto => dto.UserName))
                .ForMember(u => u.Id, cfg => cfg.MapFrom(dto => dto.Id))
                .ForMember(u => u.PasswordHash, cfg => cfg.MapFrom(dto => dto.PasswordHash));
        }
    }
}
