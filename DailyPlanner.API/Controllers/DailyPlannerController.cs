using AutoMapper;
using DailyPlanner.API.DTOs;
using DailyPlanner.Repository;
using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace DailyPlanner.API.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class DailyPlannerController : ControllerBase
{
    private readonly DailyPlannerRepository _dailyPlannerRepository;

    private readonly IMapper _mapper;

    public DailyPlannerController(DailyPlannerRepository dailyPlannerRepository, IMapper mapper)
    {
        _dailyPlannerRepository = dailyPlannerRepository;
        _mapper = mapper;
    }

    [HttpGet("{planId}")]
    public async Task<ActionResult> GetDailyPlan(int planId)
    {
        var plan = await _dailyPlannerRepository.GetDailyPlan(planId);
        if (plan == null) return NotFound();
        var mapped = _mapper.Map<DailyPlanQueryDto>(plan);
        return Ok(mapped);
    }

    [HttpGet("{lineId}/{year}/{week}")]
    public async Task<ActionResult> GetPlans(int lineId, int year, int week)
    {
        var list = await _dailyPlannerRepository.GetDailyPlans(lineId, year, week);
        var mappedPlan = _mapper.Map<DailyPlanQueryDto[]>(list);
        return Ok(mappedPlan);
    }


    [HttpPost]
    public async Task SaveDailyPlan([FromBody] DailyPlanCommandDto plan)
    {
        var mappedPlan = _mapper.Map<DailyPlan>(plan);
        await _dailyPlannerRepository.SaveDailyPlan(mappedPlan);
    }

    [HttpDelete("{planId}")]
    public async Task<ActionResult> DeletePlan(Guid planId)
    {
        await _dailyPlannerRepository.DeletePlan(planId);
        return Ok();
    }
}