using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthLab.DataAccess.Models;
using AuthLab.Domain.Entities;
using AutoMapper;

namespace AuthLab.DataAccess.Mapping;

public class UsersProfile : Profile
{
    public UsersProfile()
    {
        CreateMap<User, UserInformation>()
            .ReverseMap();
    }
}
