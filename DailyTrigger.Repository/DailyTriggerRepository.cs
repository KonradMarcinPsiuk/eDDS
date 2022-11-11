using DataLayer.Models;
using DateTimeHelpers;
using Microsoft.EntityFrameworkCore;

namespace DailyTrigger.Repository
{
    public class DailyTriggerRepository
    {
        private readonly NewbridgeContext _context;

        public DailyTriggerRepository(NewbridgeContext context)
        {
            _context = context;
        }

        /// <exception cref="OperationCanceledException"></exception>
        public async Task<IEnumerable<DailyTriggerQuestion>> GetAllQuestions()
        {
            return await _context.DailyTriggerQuestions
                .AsNoTracking()
                .Include(x=>x.ProductionLineDailyTriggerQuestions)
                .ThenInclude(x=>x.ProductionLine)
                .ToListAsync();
        }

        /// <exception cref="OperationCanceledException"></exception>
        public async Task<IEnumerable<DailyTriggerQuestion>> GetQuestionsForLine(int lineId)
        {

            return await _context.DailyTriggerQuestions
                .AsNoTracking()
                .Include(x => x.ProductionLineDailyTriggerQuestions)
                .ThenInclude(x => x.ProductionLine)
                .Where(x =>x.ProductionLineDailyTriggerQuestions.Any(z=>z.ProductionLineId==lineId)).ToListAsync();
        }

        /// <exception cref="DbUpdateException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public async Task SaveQuestion(DailyTriggerQuestion question)
        {
            _context.DailyTriggerQuestions.Update(question);
            await _context.SaveChangesAsync();
        }

        /// <exception cref="OperationCanceledException"></exception>
        public async Task<DailyTriggerQuestion?> GetQuestion(int id)
        {
            return await _context.DailyTriggerQuestions
                .AsNoTracking()
                .Include(x => x.ProductionLineDailyTriggerQuestions)
                .ThenInclude(x => x.ProductionLine)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<DailyTriggerAnswer>> GetAnswersForLineAndDate(DateOnly date, int lineId)
        {
           
            var existingAnswers = await _context.DailyTriggerAnswers.AsNoTracking().Where(x =>
                x.Date.Year==date.Year && x.Date.Month==date.Month && x.Date.Day==date.Day && x.ProductionLineId == lineId).ToListAsync();

            if (existingAnswers.Any())
                return existingAnswers;

            var questions = await GetQuestionsForLine(lineId);

            var dailyTriggerQuestions = questions as DailyTriggerQuestion[] ?? questions.ToArray();
            
            if (!dailyTriggerQuestions.Any()) return new List<DailyTriggerAnswer>();
            
            var answers = dailyTriggerQuestions.Select(q => new DailyTriggerAnswer
                {
                    Date = new DateTime(date.Year,date.Month,date.Day),
                    ProductionLineId = lineId,
                    HintText = q.Hint,
                    QuestionText = q.Question,
                    QuestionType = q.Type,
                    TargetInt = q.TargetInt,
                    Field = q.Field
                })
                .ToList();

            _context.DailyTriggerAnswers.AddRange(answers);
            await _context.SaveChangesAsync();
            return answers;

        }

        public async Task<bool> SaveProductionLineDailyTriggerLink(ProductionLineDailyTriggerQuestion link)
        {
            var checkIfExist = await _context.ProductionLineDailyTriggerQuestions.FirstOrDefaultAsync(x =>
                x.ProductionLineId == link.ProductionLineId && x.DailyTriggerQuestionId == link.DailyTriggerQuestionId);

            if (checkIfExist!=null)
                return false;
            
            _context.ProductionLineDailyTriggerQuestions.Update(link);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteProductionLineDailyTriggerLink(ProductionLineDailyTriggerQuestion link)
        {
            _context.Entry(link).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
            return true;
        }

        /// <exception cref="OperationCanceledException"></exception>
        public async Task<int> GetNumberOfQuestionsAssignedToTheLine(int lineId)
        {
            var q = await GetQuestionsForLine(lineId);
            return q.Count();
        }

        public async Task<bool> CheckIfAnswersForLineAndDateExists(DateOnly date, int lineId)
        {
            var d = new DateTime(date.Year, date.Month, date.Day);
            var a = await _context.DailyTriggerAnswers.AsNoTracking().Where(x =>
                x.Date.Year == date.Year && x.Date.Month == date.Month && x.Date.Day == date.Day &&
                x.ProductionLineId == lineId).ToListAsync();
            return a.Any();
        }

        public async Task SaveAnswer(DailyTriggerAnswer answer)
        {
            _context.DailyTriggerAnswers.Update(answer);
            await _context.SaveChangesAsync();
        }

        public async Task SaveAnswers(IEnumerable<DailyTriggerAnswer> answers)
        {
            _context.DailyTriggerAnswers.UpdateRange(answers);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<DailyTriggerAnswer>> GetAnswersForDepartmentAndDate(int deptId, DateOnly date)
        {
            return await _context.DailyTriggerAnswers.AsNoTracking().Include(x => x.ProductionLine).Where(x =>
                x.ProductionLine.DepartmentId == deptId && x.Date.Year == date.Year && x.Date.Month == date.Month &&
                x.Date.Day == date.Day).ToListAsync();
        }

    }
}
