using AutoMapper;
using DataLayer.Models;
using PlannedStop.API.DTOs;

namespace PlannedStop.API.Profiles;

public class PlannedStopProfiles : Profile
{
    public PlannedStopProfiles()
    {
        CreateMap<PmTask, PmTaskQueryDto>().ReverseMap();
        CreateMap<CilTask, CilTaskQueryDto>().ReverseMap();
        CreateMap<ClTask, ClTaskQueryDto>().ReverseMap();
        CreateMap<OtherTask, OtherTaskQueryDto>().ReverseMap();

        CreateMap<PmTask, PmTaskCommandDto>().ReverseMap();
        CreateMap<CilTask, CilTaskCommandDto>().ReverseMap();
        CreateMap<ClTask, ClTaskCommandDto>().ReverseMap();
        CreateMap<OtherTask, OtherTaskCommandDto>().ReverseMap();

        CreateMap<LineArea,LineAreaQueryDto>().ReverseMap();
    }
}