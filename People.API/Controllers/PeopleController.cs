using AutoMapper;
using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using People.API.DTOs;
using People.Repository;

namespace People.API.Controllers;

[ApiController]

public class PeopleController:ControllerBase
{
    private readonly PeopleRepository _peopleRepository;
    private IMapper _mapper;

    public PeopleController(PeopleRepository peopleRepository, IMapper mapper)
    {
        _peopleRepository = peopleRepository;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("People/GetPeople/ValueStream/{valueStreamId?}")]
    [Route("People/GetPeople/Department/{deptId?}")]
    [Route("People/GetPeople/Plant/{plantId?}")]
    public async Task<ActionResult> GetPeople(int? plantId, int? valueStreamId, int? deptId)
    {
        var ppl = await _peopleRepository.GetPeople(plantId,valueStreamId,deptId);
        var mapped = _mapper.Map<PersonDto[]>(ppl);
        return Ok(mapped);
    }


    [HttpPost]
    [Route("People/SavePerson")]
    public async Task<ActionResult> SavePerson([FromBody] PersonDto person)
    {
        var mapped = _mapper.Map<Person>(person);
        var result = await _peopleRepository.SavePerson(mapped);
        return Ok(result);
    }

    [HttpDelete]
    [Route("People/DeletePerson/{personId}")]
    public async Task<ActionResult> DeletePerson(int personId)
    {
        await _peopleRepository.DeletePerson(personId);
        return NoContent();
    }
}