using AutoMapper;
using DailyTrigger.API.DTOs;
using DataLayer.Models;

namespace DailyTrigger.API.Profiles
{
    public class Profiles:Profile
    {
        public Profiles()
        {
            CreateMap<DailyTriggerQuestion, DailyTriggerQuestionQueryDto>().ReverseMap();
            CreateMap<DailyTriggerQuestion, DailyTriggerQuestionCommandDto>().ReverseMap();
            CreateMap<ProductionLineDailyTriggerQuestion,ProductionLineDailyTriggerQuestionCommandDto>().ReverseMap();
            CreateMap<ProductionLineDailyTriggerQuestion, ProductionLineDailyTriggerQuestionQueryDto>().ReverseMap();
            CreateMap<ProductionLine, ProductionLineQueryDto>();
            CreateMap<DailyTriggerAnswer, DailyTriggerAnswerCommandDto>().ReverseMap();
            CreateMap<DailyTriggerAnswer, DailyTriggerAnswerQueryDto>().ReverseMap();

        }
    }
}
