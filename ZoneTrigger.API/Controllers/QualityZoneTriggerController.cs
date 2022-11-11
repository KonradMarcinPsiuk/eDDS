using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ZoneTrigger.Repository;

namespace ZoneTrigger.API.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class QualityZoneTriggerController:ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ZoneTriggerRepository _repository;

    public QualityZoneTriggerController(ZoneTriggerRepository zoneRepository, IMapper mapper)
    {
        _repository = zoneRepository;
        _mapper = mapper;
    }
}