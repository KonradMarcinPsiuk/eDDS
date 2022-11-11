using AutoMapper;
using DataLayer.Models;
using Defects.API.DTOs;

namespace Defects.API.Profiles;

public class DefectsProfiles : Profile
{
    public DefectsProfiles()
    {
        CreateMap<Defect, DefectQueryDto>().ReverseMap();
        CreateMap<LineArea, LineAreaQueryDto>().ReverseMap();
        CreateMap<Person, PersonQueryDto>().ReverseMap();
        CreateMap<Defect, DefectStatusDto>().ReverseMap();
    }
}