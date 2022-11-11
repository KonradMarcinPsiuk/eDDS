using AutoMapper;
using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using ZoneTrigger.API.DTOs;
using ZoneTrigger.Repository;

namespace ZoneTrigger.API.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class SafetyZoneTriggerController:ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ZoneTriggerRepository _repository;

    public SafetyZoneTriggerController(ZoneTriggerRepository zoneRepository, IMapper mapper)
    {
        _repository = zoneRepository;
        _mapper = mapper;
    }

    [HttpGet("{reportId}")]
    public async Task<ActionResult> GetSafetyZoneTrigger(int reportId)
    {
        var report = await _repository.GetSafetyZoneTrigger(reportId);
        var mapped = _mapper.Map<SafetyZoneTriggerQueryDto>(report);
        return Ok(mapped);
    }

    [HttpGet("{deptId?}")]
    public async Task<ActionResult> GetSafetyZoneTriggerQuestion(int? deptId=null)
    {
        var questions = await _repository.GetSafetyZoneTriggerQuestion(deptId);
        var mapped = _mapper.Map<SafetyZoneTriggerQuestionQueryDto[]>(questions);
        return Ok(mapped);
    }

    [HttpPost]
    public async Task<ActionResult> SaveSafetyZoneTriggerQuestion(
        [FromBody] SafetyZoneTriggerQuestionCommandDto question)
    {
        var mapped = _mapper.Map<SafetyZoneTriggerQuestion>(question);
        var result =await _repository.SaveSafetyQuestion(mapped);
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<ActionResult> SaveSafetyZoneTrigger([FromBody] SafetyZoneTriggerCommandDto report)
    {
        var mapped = _mapper.Map<SafetyZoneTrigger>(report);
        var result = await _repository.SaveSafetyZoneTrigger(mapped);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult> AddDepartmentToQuestion([FromBody] SafetyZoneTriggerQuestionDepartmentCommandDto link)
    {
        var mapped = _mapper.Map <SafetyZoneTriggerQuestionDepartment>(link);
        var result = await _repository.AddDepartmentToQuestion(mapped);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult> DeleteDepartmentLink([FromBody] SafetyZoneTriggerQuestionDepartmentCommandDto link)
    {
        var mapped = _mapper.Map <SafetyZoneTriggerQuestionDepartment>(link);
        var result = await _repository.DeleteDepartmentLink(mapped);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetSafetyQuestion(int id)
    {
        var question = await _repository.GetSafetyQuestion(id);
        var map = _mapper.Map<SafetyZoneTriggerQuestionQueryDto>(question);
        return Ok(map);
    }
}