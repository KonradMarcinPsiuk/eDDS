using AutoMapper;
using DataLayer.Models;
using PlantModel.API.DTOs;


namespace PlantModel.API.Profiles;

public class PlantModelProfiles : Profile
{
    public PlantModelProfiles()
    {
        CreateMap<Department, DepartmentQueryDto>().ReverseMap();
        CreateMap<LineArea, LineAreaQueryDto>().ReverseMap();
        CreateMap<Plant, PlantQueryDto>().ReverseMap();
        CreateMap<ProductionLine, ProductionLineQueryDto>().ReverseMap();
        CreateMap<ValueStream, ValueStreamQueryDto>().ReverseMap();
    }
}