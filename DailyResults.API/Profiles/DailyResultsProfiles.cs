using AutoMapper;
using DailyResults.API.DTOs;
using DataLayer.Models;

namespace DailyResults.API.Profiles;

public class DailyResultsProfiles : Profile
{
    public DailyResultsProfiles()
    {
        CreateMap<DailyResult, DailyResultQueryDto>().ReverseMap();
        CreateMap<DailyResult, DailyResultCommandDto>().ReverseMap();
        CreateMap<ProductionLine, ProductionLineQueryDto>().ReverseMap();
    }
}