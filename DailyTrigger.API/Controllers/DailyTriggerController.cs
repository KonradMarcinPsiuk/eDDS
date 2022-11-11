using AutoMapper;
using DailyTrigger.API.DTOs;
using DailyTrigger.Repository;
using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Exception = System.Exception;

namespace DailyTrigger.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class DailyTriggerController : ControllerBase
    {
        private readonly DailyTriggerRepository _dailyTriggerRepository;
        private readonly IMapper _mapper;

        public DailyTriggerController(DailyTriggerRepository dailyTriggerRepository, IMapper mapper)
        {
            _dailyTriggerRepository = dailyTriggerRepository;
            _mapper = mapper;
        }

        /// <exception cref="OperationCanceledException"></exception>
        [HttpGet]
        public async Task<ActionResult> GetQuestions()
        {
            var q = await _dailyTriggerRepository.GetAllQuestions();
            var mapped = _mapper.Map<IEnumerable<DailyTriggerQuestionQueryDto>>(q);
            return Ok(mapped);
        }

        [HttpGet("{questionId}")]
        public async Task<ActionResult<DailyTriggerQuestionQueryDto>> GetQuestion(int questionId)
        {
            try
            {
                var q = await _dailyTriggerRepository.GetQuestion(questionId);
                var mapped = _mapper.Map<DailyTriggerQuestionQueryDto>(q);
                return Ok(mapped);
            }
            catch (Exception ex)
            {
                //todo log
                return BadRequest();
            }
        }

        /// <exception cref="Microsoft.EntityFrameworkCore.DbUpdateException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        [HttpPost]
        public async Task<ActionResult> SaveQuestion([FromBody] DailyTriggerQuestionCommandDto question)
        {
            var mapped = _mapper.Map<DailyTriggerQuestion>(question);
            await _dailyTriggerRepository.SaveQuestion(mapped);
            return Ok();
        }

        [HttpGet("{lineId}/{date}")]
        public async Task<ActionResult> GetTriggersForLineAndDate(int lineId, DateTime date)
        {
            var answers = await _dailyTriggerRepository.GetAnswersForLineAndDate(DateOnly.FromDateTime(date), lineId);
            return Ok(answers);
        }

        [HttpPost]
        public async Task<ActionResult> SaveProductionLineDailyTriggerLink([FromBody] ProductionLineDailyTriggerQuestionCommandDto link)
        {
            var mapped = _mapper.Map<ProductionLineDailyTriggerQuestion>(link);
            var check = await _dailyTriggerRepository.SaveProductionLineDailyTriggerLink(mapped);
            if(check)return Ok();
            return BadRequest();
        }
        
        [HttpPost]
        public async Task<ActionResult> DeleteProductionLineDailyTriggerLink([FromBody] ProductionLineDailyTriggerQuestionCommandDto link)
        {
            var mapped = _mapper.Map<ProductionLineDailyTriggerQuestion>(link);
            var check = await _dailyTriggerRepository.DeleteProductionLineDailyTriggerLink(mapped);
            if(check) return Ok();
            return BadRequest();
        }

        [HttpGet("{lineId}")]
        public async Task<ActionResult> GetNumberOfQuestionsAssignedToTheLine(int lineId)
        {
            return Ok(await _dailyTriggerRepository.GetNumberOfQuestionsAssignedToTheLine(lineId));
        }

        [HttpGet("{date}/{lineId}")]
        public async Task<ActionResult<bool>> CheckIfAnswersForLineAndDateExists(DateTime date, int lineId)
        {
            return Ok(await _dailyTriggerRepository.CheckIfAnswersForLineAndDateExists(DateOnly.FromDateTime(date), lineId));
        }

        [HttpPost]
        public async Task<ActionResult> SaveAnswer([FromBody] DailyTriggerAnswerCommandDto answer)
        {
            var mapped = _mapper.Map<DailyTriggerAnswer>(answer);
            await _dailyTriggerRepository.SaveAnswer(mapped);
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> SaveAnswers([FromBody] IEnumerable<DailyTriggerAnswerCommandDto> answers)
        {
            var mapped = _mapper.Map<IEnumerable<DailyTriggerAnswer>>(answers);
            await _dailyTriggerRepository.SaveAnswers(mapped);
            return Ok();
        }

        [HttpGet("{date}/{deptId}")]
        public async Task<ActionResult> GetAnswersForDepartmentAndDate(DateTime date, int deptId)
        {
          var a =await _dailyTriggerRepository.GetAnswersForDepartmentAndDate(deptId, DateOnly.FromDateTime(date));
          var mapped = _mapper.Map<IEnumerable<DailyTriggerAnswerQueryDto>>(a);
          return Ok(mapped);
        }
    }
}
