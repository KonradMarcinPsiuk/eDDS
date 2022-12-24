using AutoMapper;
using DataLayer.Models;
using DTOs;
using Microsoft.AspNetCore.Mvc;
using TechnicalAssessment.Repository;

namespace TechnicalAssessment.API.Controllers
{

    [ApiController]
    [Route("[controller]/[action]")]
    public class TechnicalAssessmentController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly TechnicalAssessmentRepository _technicalAssessmentRepository;

        public TechnicalAssessmentController(TechnicalAssessmentRepository technicalAssessmentRepository,
            IMapper mapper)
        {
            _technicalAssessmentRepository = technicalAssessmentRepository;
            _mapper = mapper;
        }

        [HttpGet("{transformationId:int}")]
        public async Task<ActionResult> GetTransformation(int transformationId)
        {
            var result = await _technicalAssessmentRepository.GetTransformation(transformationId);
            var mapped = _mapper.Map<TransformationDto>(result);
            return Ok(mapped);
        }

        [HttpPost]
        public async Task<ActionResult> SaveTransformation([FromBody] TransformationDto transformation)
        {
            var mapped = _mapper.Map<Transformation>(transformation);
            return Ok(await _technicalAssessmentRepository.SaveTransformation(mapped));
        }

        [HttpGet("{lineAreaId?}")]
        public async Task<ActionResult> GetTransformations(int? lineAreaId = null)
        {
            var result = await _technicalAssessmentRepository.GetTransformations(lineAreaId);
            var mapped = _mapper.Map<TransformationDto[]>(result);
            return Ok(mapped);
        }

        [HttpDelete("{actionId}")]
        public async Task<ActionResult> DeleteAction(Guid actionId)
        {
            await _technicalAssessmentRepository.DeleteAction(actionId);
            return NoContent();
        }

        [HttpDelete("{componentId}")]
        public async Task<ActionResult> DeleteComponent(int componentId)
        {
            await _technicalAssessmentRepository.DeleteComponent(componentId);
            return NoContent();
        }

        [HttpDelete("{transformationId}")]
        public async Task<ActionResult> DeleteTransformation(int transformationId)
        {
            await _technicalAssessmentRepository.DeleteTransformation(transformationId);
            return NoContent();
        }
    }
}