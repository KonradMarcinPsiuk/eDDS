using AutoMapper;
using DataLayer.Models;
using People.API.DTOs;

namespace People.API.Profiles;

public class PeopleProfiles:Profile
{
    public PeopleProfiles()
    {
        CreateMap<Person, PersonDto>().ReverseMap();
        CreateMap<Department, DepartmentDto>().ReverseMap();
    }
}