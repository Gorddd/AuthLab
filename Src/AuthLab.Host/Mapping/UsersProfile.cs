using AuthLab.Domain.Entities;
using AuthLab.Host.Models;
using AutoMapper;

namespace AuthLab.Host.Mapping;

public class UsersProfile : Profile
{
    public UsersProfile()
    {
        CreateMap<User, UserValidationInformation>()
            .ReverseMap();
    }
}
