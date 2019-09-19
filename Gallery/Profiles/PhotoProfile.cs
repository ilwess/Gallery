using AutoMapper;
using BLL.DTO;
using Domain.Models;

namespace Gallery.Profiles
{
    public class PhotoProfile : Profile
    {
        public PhotoProfile()
        {
            CreateMap<Photo, PhotoDTO>()
                .ForMember(dto => dto.Id, cfg => cfg.MapFrom(p => p.Id))
                .ForMember(dto => dto.Name, cfg => cfg.MapFrom(p => p.Name))
                .ForMember(dto => dto.ImageLink, cfg => cfg.MapFrom(p => p.ImageLink))
                .ForMember(dto => dto.User, cfg => cfg.MapFrom(p => p.User));
            CreateMap<PhotoDTO, Photo>()
                .ForMember(dto => dto.Name, cfg => cfg.MapFrom(p => p.Name))
                .ForMember(dto => dto.ImageLink, cfg => cfg.MapFrom(p => p.ImageLink))
                .ForMember(dto => dto.User, cfg => cfg.MapFrom(p => p.User));
        }
    }
}
