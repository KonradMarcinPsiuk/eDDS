using AutoMapper;
using DailyResults.API.DTOs;
using DailyResults.Repository;
using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace DailyResults.API.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class DailyResultsController : ControllerBase
{
  
    private readonly IMapper _mapper;
    private readonly DailyResultsRepository _dailyResultsRepository;

    public DailyResultsController(DailyResultsRepository dailyResultsRepository,
        IMapper mapper)
    {
        _dailyResultsRepository = dailyResultsRepository;
        _mapper = mapper;
    }

    [HttpGet("{date}/{lineId}")]
    public async Task<ActionResult<DailyResultQueryDto>> GetResultForLineAndDate(DateTime date, int lineId)
    {
        var dr = await _dailyResultsRepository.GetResultForLineAndDate(date, lineId);
        var mapped = _mapper.Map<DailyResultQueryDto>(dr);
        return Ok(mapped);
    }

    [HttpGet("{date}/{deptId}")]
    public async Task<ActionResult<IEnumerable<DailyResultQueryDto>>> GetResultsForDateAndDepartment(DateTime date, int deptId)
    {
        var results = await _dailyResultsRepository.GetResultsForDateAndDepartment(date, deptId);
        var mapped = _mapper.Map<IEnumerable<DailyResultQueryDto>>(results);
        return Ok(mapped);
    }

    [HttpGet("{date}/{lineId}")]
    public async Task<ActionResult<bool>> CheckIfResultForLineAndDateExists(DateTime date, int lineId)
    {
        return Ok(await _dailyResultsRepository.CheckIfResultForLineAndDateExists(date, lineId));
    }

    [HttpPost]
    public async Task<ActionResult> SaveDailyResult([FromBody] DailyResultCommandDto results)
    {
        var mapped = _mapper.Map<DailyResult>(results);
        await _dailyResultsRepository.SaveDailyResult(mapped);
        return Ok();
    }

 

}