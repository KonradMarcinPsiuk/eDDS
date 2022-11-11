using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlantModel.API.DTOs;
using PlantModel.Repository;

namespace PlantModel.API.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class PlantModelController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly PlantModelRepository _plantModelRepository;

    public PlantModelController(PlantModelRepository plantModelRepository, IMapper mapper)
    {
        _plantModelRepository = plantModelRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult> GetPlants()
    {
        var list = await _plantModelRepository.GetAllPlants();
        return Ok(_mapper.Map<IEnumerable<PlantQueryDto>>(list));
    }

    [HttpGet("{plantId:int}")]
    public async Task<ActionResult> GetValueStreamsForPlant(int plantId)
    {
        var list = await _plantModelRepository.GetValueStreamsForPlant(plantId);
        return Ok(_mapper.Map<IEnumerable<ValueStreamQueryDto>>(list));
    }

    [HttpGet("{vsId:int}")]
    public async Task<ActionResult> GetDepartmentsForValueStream(int vsId)
    {
        var list = await _plantModelRepository.GetDepartmentsForValueStream(vsId);
        return Ok(_mapper.Map<IEnumerable<DepartmentQueryDto>>(list));
    }
    
    [HttpGet("{plantId:int}")]
    public async Task<ActionResult> GetDepartmentsForPlant(int plantId)
    {
        var list = await _plantModelRepository.GetDepartmentsForPlant(plantId);
        return Ok(_mapper.Map<IEnumerable<DepartmentQueryDto>>(list));
    }

    [HttpGet("{departmentId:int}")]
    public async Task<ActionResult> GetLinesForDepartment(int departmentId)
    {
        var list = await _plantModelRepository.GetProductionLinesForDepartment(departmentId);
        return Ok(_mapper.Map<IEnumerable<ProductionLineQueryDto>>(list));
    }

    [HttpGet("{lineId:int}")]
    public async Task<ActionResult> GetLineAreasForLine(int lineId)
    {
        var list = await _plantModelRepository.GetLineAreasForLine(lineId);
        return Ok(_mapper.Map<IEnumerable<LineAreaQueryDto>>(list));
    }
}