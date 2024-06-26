﻿using Agenda.Domain.Entity;
using Agenda.Web.ViewModel;

namespace Agenda.Web.Mapper
{
    public class UserMapper : AutoMapper.Profile
    {
        public UserMapper()
        {
            CreateMap<UserVM, User>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => $"{src.Id}")).ReverseMap().ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.Name}")).ReverseMap().ForMember(dest => dest.Name, opt => opt.Ignore())
                .ForMember(dest => dest.Cnpj, opt => opt.MapFrom(src => $"{src.Cnpj}")).ReverseMap().ForMember(dest => dest.Cnpj, opt => opt.Ignore())
                .ForMember(dest => dest.Representation, opt => opt.MapFrom(src => $"{src.Representation}")).ReverseMap().ForMember(dest => dest.Representation, opt => opt.Ignore())
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => $"{src.Email}")).ReverseMap().ForMember(dest => dest.Email, opt => opt.Ignore())
                .ForMember(dest => dest.Login, opt => opt.MapFrom(src => $"{src.Login}")).ReverseMap().ForMember(dest => dest.Login, opt => opt.Ignore())
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => $"{src.Password}")).ReverseMap().ForMember(dest => dest.Password, opt => opt.Ignore())
                .ForMember(dest => dest.Profile, opt => opt.MapFrom(src => $"{src.Profile}")).ReverseMap().ForMember(dest => dest.Profile, opt => opt.Ignore())
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => $"{src.Active}")).ReverseMap().ForMember(dest => dest.Active, opt => opt.Ignore())
                .ForMember(dest => dest.BranchWithoutAdm, opt => opt.MapFrom(src => $"{src.BranchWithoutAdm}")).ReverseMap().ForMember(dest => dest.BranchWithoutAdm, opt => opt.Ignore()); 
        }
    }
}
