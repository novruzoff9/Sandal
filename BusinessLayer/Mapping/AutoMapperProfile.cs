﻿using AutoMapper;
using EntityLayer.Concrete;
using EntityLayer.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Mapping;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<User, UserRegisterDto>().ReverseMap();
        CreateMap<User, UserListDto>().ReverseMap();
        CreateMap<User, UserDetailsDto>().ReverseMap();
    }
}
