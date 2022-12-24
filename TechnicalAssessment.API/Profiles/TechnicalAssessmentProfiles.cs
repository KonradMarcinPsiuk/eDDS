using AutoMapper;
using DataLayer.Models;
using DTOs;

namespace TechnicalAssessment.API.Profiles
{
    public class TechnicalAssessmentProfiles : Profile
    {
        public TechnicalAssessmentProfiles()
        {
            CreateMap<Transformation, TransformationDto>().ReverseMap();
            CreateMap<LineArea, LineAreaDto>().ReverseMap();
            CreateMap<Component, ComponentDto>().ReverseMap();
            CreateMap<ComponentAction, ComponentActionDto>().ReverseMap();
            CreateMap<Person, PersonDto>().ReverseMap();
        }
    }
}
