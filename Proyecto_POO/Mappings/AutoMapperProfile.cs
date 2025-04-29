using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Proyecto_POO.DTOs;
using Proyecto_POO.Models;

namespace Proyecto_POO.Mappings;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<UsuarioRegisterDTO, Person>();
        CreateMap<Person, PersonDTO>().ReverseMap();
        CreateMap<User, UserDTO>();
    }
}
