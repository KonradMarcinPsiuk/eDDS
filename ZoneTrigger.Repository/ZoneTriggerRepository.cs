using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace ZoneTrigger.Repository;

public class ZoneTriggerRepository

{ private readonly NewbridgeContext _context;

    public ZoneTriggerRepository(NewbridgeContext context)
    {
        _context = context;
    }

    public async Task<SafetyZoneTrigger> GetSafetyZoneTrigger(int id)
    {
        var zt = await _context.SafetyZoneTriggers
            .AsNoTracking()
            .Include(x=>x.SafetyZoneTriggerAnswers)
            .Include(x=>x.Department)
            .FirstOrDefaultAsync(x => x.Id == id);
        return zt;
    }

    public async Task<IEnumerable<SafetyZoneTriggerQuestion>> GetSafetyZoneTriggerQuestion(int? deptId = null)
    {
        var query =  _context.SafetyZoneTriggerQuestions
            .Include(x=>x.SafetyZoneTriggerQuestionDepartments)
            .ThenInclude(x=>x.Department)
            .AsNoTracking();

        if (deptId != null)
            query = query.Where(x => x.SafetyZoneTriggerQuestionDepartments.Any(z => z.DepartmentId == deptId));

        var questions = await query.ToListAsync();
        return questions;
    }

    public async Task<int> SaveSafetyQuestion(SafetyZoneTriggerQuestion question)
    {
        _context.SafetyZoneTriggerQuestions.Update(question);
        await _context.SaveChangesAsync();
        return question.Id;
    }

    public async Task<int> SaveSafetyZoneTrigger(SafetyZoneTrigger trigger)
    {
        _context.SafetyZoneTriggers.Update(trigger);
        await _context.SaveChangesAsync();
        return trigger.Id;
    }

    public async Task<int> AddDepartmentToQuestion(SafetyZoneTriggerQuestionDepartment link)
    {
        _context.SafetyZoneTriggerQuestionDepartments.Update(link);
        await _context.SaveChangesAsync();
        return link.Id;
    }

    public async Task<bool> DeleteDepartmentLink(SafetyZoneTriggerQuestionDepartment link)
    {
        _context.Entry(link).State = EntityState.Deleted;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<SafetyZoneTriggerQuestion> GetSafetyQuestion(int id)
    {
        var question = await _context.SafetyZoneTriggerQuestions
            .AsNoTracking()
            .Include(x=>x.SafetyZoneTriggerQuestionDepartments)
            .ThenInclude(x => x.Department)
            .FirstAsync(x => x.Id == id);
        return question;
    }
}