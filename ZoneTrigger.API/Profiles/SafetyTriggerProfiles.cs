using AutoMapper;
using DataLayer.Models;
using ZoneTrigger.API.DTOs;

namespace ZoneTrigger.API.Profiles;

public class SafetyTriggerProfiles:Profile
{
    public SafetyTriggerProfiles()
    {
        CreateMap<DepartmentQueryDto, Department>().ReverseMap();
        CreateMap<SafetyZoneTriggerAnswerCommandDto, SafetyZoneTriggerAnswer>().ReverseMap();
        CreateMap<SafetyZoneTriggerAnswerQueryDto, SafetyZoneTriggerAnswer>().ReverseMap();
        CreateMap<SafetyZoneTriggerCommandDto,SafetyZoneTrigger>().ReverseMap();
        CreateMap<SafetyZoneTriggerQueryDto, SafetyZoneTrigger>().ReverseMap();
        CreateMap<SafetyZoneTriggerQuestionQueryDto,SafetyZoneTriggerQuestion>().ReverseMap();
        CreateMap<SafetyZoneTriggerQuestionCommandDto,SafetyZoneTriggerQuestion>().ReverseMap();
        CreateMap<SafetyZoneTriggerQuestionDepartmentCommandDto,SafetyZoneTriggerQuestionDepartment>().ReverseMap();
        CreateMap<SafetyZoneTriggerQuestionDepartmentQueryDto,SafetyZoneTriggerQuestionDepartment>().ReverseMap();
    }
    
}