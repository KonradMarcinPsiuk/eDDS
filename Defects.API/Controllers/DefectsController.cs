using System.Reflection.Metadata.Ecma335;
using AutoMapper;
using DataLayer.Models;
using Defects.API.DTOs;
using Defects.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Defects.API.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class DefectsController : ControllerBase
{
    private readonly DefectsRepository _defectsRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<DefectsController> _logger;

    public DefectsController(DefectsRepository defectsRepository, IMapper mapper, ILogger<DefectsController> logger)
    {
        _defectsRepository = defectsRepository;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet("{defectId:guid}")]
    public async Task<ActionResult> GetDefect(Guid defectId)
    {
        var defect = await _defectsRepository.GetDefect(defectId);

        if (defect == null)
            return NoContent();

        var mapped = _mapper.Map<DefectQueryDto>(defect);
        return Ok(mapped);
    }

    [HttpPost]
    public async Task SaveDefect([FromBody] DefectQueryDto defect)
    {
        defect.LineArea = null;
        defect.Owner = null;
        var d = _mapper.Map<Defect>(defect);
        await _defectsRepository.AttachDefect(d);
        await _defectsRepository.SaveContext();
    }

    [HttpGet("{lineId?}/{areaId?}/{closedOnly?}")]
    public async Task<ActionResult> GetDefects(int? lineId = null, int? areaId = null, bool closedOnly = false)
    {
        try
        {
            var defects = await _defectsRepository.GetDefects(lineId, areaId, closedOnly);
            var mapped = _mapper.Map<IEnumerable<DefectQueryDto>>(defects);
            return Ok(mapped);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest();
        }
       
    }
    
    [HttpGet("{lineId?}/{status}")]
    public async Task<ActionResult> GetDefectsForLine(int lineId,  string status)
    {
        var defects = await _defectsRepository.GetDefectsForLine(lineId, status);
        var mapped = _mapper.Map<IEnumerable<DefectQueryDto>>(defects);
        return Ok(mapped);
    }
    

    [HttpPut]
    public async Task<ActionResult> CloseDefect([FromBody] DefectStatusDto defect)
    {
        await _defectsRepository.MarkDefectAsClosed(defect.Id);
        await _defectsRepository.SaveContext();
        return NoContent();
    }

    [HttpPut]
    public async Task<ActionResult> OpenDefect([FromBody] DefectStatusDto defect)
    {
        await _defectsRepository.MarkDefectAsOpen(defect.Id);
        await _defectsRepository.SaveContext();
        return NoContent();
    }

    [HttpDelete("defectId")]

    public async Task<ActionResult> DeleteDefect(Guid defectId)
    {
        await _defectsRepository.DeleteDefect(defectId);
        return NoContent();
    }
}