using AutoMapper;
using DailyPlanner.API.DTOs;
using DataLayer.Models;

namespace DailyPlanner.API.Profiles;

public class DailyPlannerProfiles : Profile
{
    public DailyPlannerProfiles()
    {

        CreateMap<TimeOnly,TimeSpan>().ConvertUsing(new TimeOnlyTimeSpan());
        CreateMap<TimeSpan, TimeOnly>().ConvertUsing(new TimeSpanTimeOnly());


        CreateMap<DailyPlan, DailyPlanQueryDto>()
            .ForMember(x=>x.StartDate, opt=>opt.MapFrom<DailyPlanQueryDtoDateOnly>())
            .ReverseMap()
            .ForMember(x=>x.StartDate, opt=>opt.MapFrom<DailyPlanQueryDtoDateOnlyReverse>());

        CreateMap<DailyPlan, DailyPlanCommandDto>()
            .ForMember(x=>x.StartDate, opt=>opt.MapFrom<DailyPlanCommandDtoDateOnly>())
            .ReverseMap()
            .ForMember(x=>x.StartDate, opt=>opt.MapFrom<DailyPlanCommandDtoDateOnlyReverse>());

        CreateMap<DailyPlanDefectTask, DailyPlanDefectTaskCommandDto>()
            .ForMember(x=>x.StartTime,opt=>opt.MapFrom<DefectTaskDtoTimeOnly>())
            .ReverseMap()
            .ForMember(x=>x.StartTime,opt=>opt.MapFrom<DefectTaskDtoTimeOnlyReverse>());

        CreateMap<DailyPlanDefectTask, DailyPlanDefectTaskQueryDto>()
            .ForMember(x => x.StartTime, opt => opt.MapFrom<DefectTaskQueryDtoTimeOnly>())
            .ReverseMap()
             .ForMember(x => x.StartTime, opt => opt.MapFrom<DefectTaskQueryDtoTimeOnlyReverse>());

        CreateMap<DailyPlanPmTask, DailyPlanPmTaskCommandDto>().ReverseMap();
        CreateMap<DailyPlanPmTask, DailyPlanPmTaskQueryDto>().ReverseMap();

        CreateMap<DailyPlanCilTask, DailyPlanCilTaskCommandDto>().ReverseMap();
        CreateMap<DailyPlanCilTask, DailyPlanCilTaskQueryDto>().ReverseMap();

        CreateMap<DailyPlanClTask, DailyPlanClTaskCommandDto>().ReverseMap();
        CreateMap<DailyPlanClTask, DailyPlanClTaskQueryDto>().ReverseMap();

        CreateMap<DailyPlanOtherTask, DailyPlanOtherTaskCommandDto>().ReverseMap();
        CreateMap<DailyPlanOtherTask, DailyPlanOtherTaskQueryDto>().ReverseMap();


        CreateMap<Defect, DefectDto>().ReverseMap();
        CreateMap<ProductionLine, ProductionLineDto>().ReverseMap();
        CreateMap<LineArea, LineAreaDto>().ReverseMap();
        CreateMap<Person, PersonQueryDto>().ReverseMap();

        CreateMap<PmTask, PmTaskDto>().ReverseMap();
        CreateMap<CilTask, CilTaskDto>().ReverseMap();
        CreateMap<ClTask, ClTaskDto>().ReverseMap();
        CreateMap<OtherTask, OtherTaskDto>().ReverseMap();
    }
}

internal class TimeSpanTimeOnly : ITypeConverter<TimeSpan, TimeOnly>
{
    public TimeOnly Convert(TimeSpan source, TimeOnly destination, ResolutionContext context)
    {
        return TimeOnly.FromTimeSpan(source);
    }
}

internal class TimeOnlyTimeSpan : ITypeConverter<TimeOnly, TimeSpan>
{
    public TimeSpan Convert(TimeOnly source, TimeSpan destination, ResolutionContext context)
    {
        return new TimeSpan(source.Hour, source.Minute, source.Second);
    }
}


internal class DefectTaskQueryDtoTimeOnly : IValueResolver<DailyPlanDefectTask, DailyPlanDefectTaskQueryDto, TimeOnly?>
{
    public TimeOnly? Resolve(DailyPlanDefectTask source, DailyPlanDefectTaskQueryDto destination, TimeOnly? destMember, ResolutionContext context)
    {
        return source.StartTime != null ? TimeOnly.FromTimeSpan((TimeSpan)source.StartTime) : null;
    }
}

internal class DefectTaskQueryDtoTimeOnlyReverse : IValueResolver<DailyPlanDefectTaskQueryDto, DailyPlanDefectTask, TimeSpan?>
{
    public TimeSpan? Resolve(DailyPlanDefectTaskQueryDto source, DailyPlanDefectTask destination, TimeSpan? destMember, ResolutionContext context)
    {
        if (source.StartTime == null) return null;
        var t = (TimeOnly)source.StartTime;
        return new TimeSpan(t.Hour, t.Minute, t.Second);
    }
}

internal class DefectTaskDtoTimeOnly : IValueResolver<DailyPlanDefectTask, DailyPlanDefectTaskCommandDto, TimeOnly?>
{
    public TimeOnly? Resolve(DailyPlanDefectTask source, DailyPlanDefectTaskCommandDto destination, TimeOnly? destMember, ResolutionContext context)
    {
        return source.StartTime!=null ? TimeOnly.FromTimeSpan((TimeSpan)source.StartTime) : null;
    }
}

internal class DefectTaskDtoTimeOnlyReverse : IValueResolver<DailyPlanDefectTaskCommandDto, DailyPlanDefectTask, TimeSpan?>
{
    public TimeSpan? Resolve(DailyPlanDefectTaskCommandDto source, DailyPlanDefectTask destination, TimeSpan? destMember, ResolutionContext context)
    {
        if (source.StartTime == null) return null;
        var t = (TimeOnly)source.StartTime;
        return new TimeSpan(t.Hour,t.Minute, t.Second);
    }
}

internal class DailyPlanQueryDtoDateOnly : IValueResolver<DailyPlan, DailyPlanQueryDto, DateOnly>
{
    public DateOnly Resolve(DailyPlan source, DailyPlanQueryDto destination, DateOnly destMember, ResolutionContext context)
    {
        return DateOnly.FromDateTime(source.StartDate);
    }
}

internal class DailyPlanQueryDtoDateOnlyReverse : IValueResolver<DailyPlanQueryDto, DailyPlan, DateTime>
{
    public DateTime Resolve(DailyPlanQueryDto source, DailyPlan destination, DateTime destMember, ResolutionContext context)
    {
        return new DateTime(source.StartDate.Year, source.StartDate.Month, source.StartDate.Day);
    }
}

internal class DailyPlanCommandDtoDateOnly : IValueResolver<DailyPlan, DailyPlanCommandDto, DateOnly>
{
    public DateOnly Resolve(DailyPlan source, DailyPlanCommandDto destination, DateOnly destMember, ResolutionContext context)
    {
        return DateOnly.FromDateTime(source.StartDate);
    }
}

internal class DailyPlanCommandDtoDateOnlyReverse : IValueResolver<DailyPlanCommandDto, DailyPlan, DateTime>
{
    public DateTime Resolve(DailyPlanCommandDto source, DailyPlan destination, DateTime destMember, ResolutionContext context)
    {
        return new DateTime(source.StartDate.Year, source.StartDate.Month, source.StartDate.Day);
    }
}