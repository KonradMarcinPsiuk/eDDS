using AutoMapper;
using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using PlannedStop.API.DTOs;
using PlannedStop.Repository;

namespace PlannedStop.API.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class PlannedStopController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly PlannedStopRepository _plannedStopRepository;

    public PlannedStopController(PlannedStopRepository plannedStopRepository, IMapper mapper)
    {
        _plannedStopRepository = plannedStopRepository;
        _mapper = mapper;
    }


    [HttpGet("{taskId}")]
    public async Task<ActionResult> GetPmTask(Guid taskId)
    {
        var t = await _plannedStopRepository.GetPmTask(taskId);
        var mapped = _mapper.Map<PmTaskQueryDto>(t);
        return Ok(mapped);
    }

    [HttpGet("{taskId}")]
    public async Task<ActionResult> GetCilTask(Guid taskId)
    {
        var t =await _plannedStopRepository.GetCilTask(taskId);
        var mapped = _mapper.Map<CilTaskQueryDto>(t);
        return Ok(mapped);
    }

    [HttpGet("{taskId}")]
    public async Task<ActionResult> GetClTask(Guid taskId)
    {
        var t = await _plannedStopRepository.GetClTask(taskId);
        var mapped = _mapper.Map<ClTaskQueryDto>(t);
        return Ok(mapped);
    }

    [HttpGet("{taskId}")]
    public async Task<ActionResult> GetOtherTask(Guid taskId)
    {
        var t = await _plannedStopRepository.GetOtherTask(taskId);
        var mapped = _mapper.Map<OtherTaskQueryDto>(t);
        return Ok(mapped);
    }

    [HttpGet("{lineId}/{openOnly}")]
    public async Task<ActionResult> GetPmTasksForLine(int lineId, bool openOnly)
    {
        var tasks = await _plannedStopRepository.GetPmTasks(lineId, openOnly);
        var mapped = _mapper.Map<IEnumerable<PmTaskQueryDto>>(tasks);
        return Ok(mapped);
    }

    [HttpGet("{lineId}/{openOnly}")]
    public async Task<ActionResult> GetCilTasksForLine(int lineId, bool openOnly)
    {
        var tasks = await _plannedStopRepository.GetCilTasks(lineId, openOnly);
        var mapped = _mapper.Map<IEnumerable<CilTaskQueryDto>>(tasks);
        return Ok(mapped);
    }

    [HttpGet("{lineId}/{openOnly}")]
    public async Task<ActionResult> GetClTasksForLine(int lineId, bool openOnly)
    {
        var tasks = await _plannedStopRepository.GetClTasks(lineId, openOnly);
        var mapped = _mapper.Map<IEnumerable<ClTaskQueryDto>>(tasks);
        return Ok(mapped);
    }

    [HttpGet("{lineId}/{openOnly}")]
    public async Task<ActionResult> GetOtherTasksForLine(int lineId, bool openOnly)
    {
        var tasks = await _plannedStopRepository.GetOtherTasks(lineId, openOnly);
        var mapped = _mapper.Map<IEnumerable<OtherTaskQueryDto>>(tasks);
        return Ok(mapped);
    }

    [HttpPost]
    public async Task<ActionResult> SavePmTask([FromBody] PmTaskCommandDto task)
    {
        var mapped = _mapper.Map<PmTask>(task);
        await _plannedStopRepository.AttachPmTask(mapped);
        await _plannedStopRepository.SaveContext();
        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult> SaveClTask([FromBody] ClTaskCommandDto task)
    {
        var mapped = _mapper.Map<ClTask>(task);
               await _plannedStopRepository.AttachClTask(mapped);
        await _plannedStopRepository.SaveContext();
        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult> SaveCilTask([FromBody] CilTaskCommandDto task)
    {
        var mapped = _mapper.Map<CilTask>(task);
        await _plannedStopRepository.AttachCilTask(mapped);
        await _plannedStopRepository.SaveContext();
        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult> SaveOtherTask([FromBody] OtherTaskCommandDto task)
    {
        var mapped = _mapper.Map<OtherTask>(task);
        await _plannedStopRepository.AttachOtherTask(mapped);
        await _plannedStopRepository.SaveContext();
        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult> SavePmTasks([FromBody] IEnumerable<PmTaskCommandDto> tasks)
    {
        var mapped = _mapper.Map<IEnumerable<PmTask>>(tasks);
        foreach (var t in mapped)
        {
            await _plannedStopRepository.AttachPmTask(t);
        }

        await _plannedStopRepository.SaveContext();
        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult> SaveClTasks([FromBody] IEnumerable<ClTaskCommandDto> tasks)
    {
        var mapped = _mapper.Map<IEnumerable<ClTask>>(tasks);
        foreach (var t in mapped)
        {
            await _plannedStopRepository.AttachClTask(t);
        }

        await _plannedStopRepository.SaveContext();
        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult> SaveCilTasks([FromBody] IEnumerable<CilTaskCommandDto> tasks)
    {
        var mapped = _mapper.Map<IEnumerable<CilTask>>(tasks);
        foreach (var t in mapped)
        {
            await _plannedStopRepository.AttachCilTask(t);
        }

        await _plannedStopRepository.SaveContext();
        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult> SaveOtherTasks([FromBody] IEnumerable<OtherTaskCommandDto> tasks)
    {
        var mapped = _mapper.Map<IEnumerable<OtherTask>>(tasks);
        foreach (var t in mapped)
        {
            await _plannedStopRepository.AttachOtherTask(t);
        }

        await _plannedStopRepository.SaveContext();
        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult> DeletePmTask([FromBody]Guid taskId)
    {
        await _plannedStopRepository.DeletePmTask(taskId);
        return NoContent();
    }

    [HttpPost("taskId")]
    public async Task<ActionResult> DeleteCilTask(Guid taskId)
    {
        await _plannedStopRepository.DeleteCilTask(taskId);
        return NoContent();
    }

    [HttpPost("taskId")]
    public async Task<ActionResult> DeleteClTask(Guid taskId)
    {
        await _plannedStopRepository.DeleteClTask(taskId);
        return NoContent();
    }

    [HttpPost("taskId")]
    public async Task<ActionResult> DeleteOtherTask(Guid taskId)
    {
        await _plannedStopRepository.DeleteOtherTask(taskId);
        return NoContent();
    }


}