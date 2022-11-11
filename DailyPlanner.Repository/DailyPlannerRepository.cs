using DataLayer.Models;
using DateTimeHelpers;
using Microsoft.EntityFrameworkCore;

namespace DailyPlanner.Repository;

public class DailyPlannerRepository
{
    private readonly NewbridgeContext _context;

    public DailyPlannerRepository(NewbridgeContext context)
    {
        _context = context;
    }

    public async Task<DailyPlan?> GetDailyPlan(int planId)
    {
        return await _context.DailyPlans
            .Include(x => x.ProductionLine)
            .Include(x => x.DailyPlanDefectTasks)
            .ThenInclude(x => x.LinkedTask)
            .ThenInclude(x => x.LineArea)
            .Include(x => x.DailyPlanDefectTasks)
            .ThenInclude(x => x.Owner)
            .Include(x => x.DailyPlanClTasks)
            .ThenInclude(x => x.LinkedTask)
            .ThenInclude(x => x.LineArea)
            .Include(x => x.DailyPlanClTasks)
            .ThenInclude(x => x.Owner)
            .Include(x => x.DailyPlanCilTasks)
            .ThenInclude(x => x.LinkedTask)
            .ThenInclude(x => x.LineArea)
            .Include(x => x.DailyPlanCilTasks)
            .ThenInclude(x => x.Owner)
            .Include(x => x.DailyPlanPmTasks)
            .ThenInclude(x => x.LinkedTask)
            .ThenInclude(x => x.LineArea)
            .Include(x => x.DailyPlanPmTasks)
            .ThenInclude(x => x.Owner)
            .Include(x => x.DailyPlanOtherTasks)
            .ThenInclude(x => x.LinkedTask)
            .ThenInclude(x => x.LineArea)
            .Include(x => x.DailyPlanOtherTasks)
            .ThenInclude(x => x.Owner)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == planId);
    }

    public async Task<IEnumerable<DailyPlan>> GetDailyPlans(int lineId, int? year, int? week)
    {
        var query = _context.DailyPlans.AsNoTracking()
            .Include(x => x.ProductionLine)
            .Include(x => x.DailyPlanDefectTasks)
            .ThenInclude(x => x.LinkedTask)
            .Include(x => x.DailyPlanClTasks)
            .ThenInclude(x => x.LinkedTask)
            .Include(x => x.DailyPlanCilTasks)
            .ThenInclude(x => x.LinkedTask)
            .Include(x => x.DailyPlanPmTasks)
            .ThenInclude(x => x.LinkedTask)
            .Include(x => x.DailyPlanOtherTasks)
            .ThenInclude(x => x.LinkedTask)
            .Where(x => x.ProductionLineId == lineId);

        if (year != null)
        {
            var startYear = new DateTime((int) year, 1, 1);
            var endYear = startYear.AddYears(1);
            query = query.Where(x => x.StartDate.Year >= startYear.Year && x.StartDate.Year < endYear.Year);

            if (week != null)
            {
                var start = WeekNumberCalculations.GetFirstDayOfTheWeek((int) week, (int) year);
                var end = WeekNumberCalculations.GetLastDayOfTheWeek((int) week, (int) year);
                query = query.Where(x => x.StartDate >= start && x.StartDate < end);
            }
        }

        var list = await query.ToListAsync();
        return list;
    }


    public async Task SaveDailyPlan(DailyPlan plan)
    {
        if (plan.Id != 0)
        {
            var storedPlan = await GetDailyPlan(plan.Id);
            if (storedPlan != null)
            {
                foreach (var task in storedPlan.DailyPlanDefectTasks)
                    if (plan.DailyPlanDefectTasks.All(x => x.Id != task.Id))
                        _context.Entry(task).State = EntityState.Deleted;

                foreach (var task in storedPlan.DailyPlanCilTasks)
                    if (plan.DailyPlanCilTasks.All(x => x.Id != task.Id))
                        _context.Entry(task).State = EntityState.Deleted;

                foreach (var task in storedPlan.DailyPlanClTasks)
                    if (plan.DailyPlanClTasks.All(x => x.Id != task.Id))
                        _context.Entry(task).State = EntityState.Deleted;

                foreach (var task in storedPlan.DailyPlanPmTasks)
                    if (plan.DailyPlanPmTasks.All(x => x.Id != task.Id))
                        _context.Entry(task).State = EntityState.Deleted;

                foreach (var task in storedPlan.DailyPlanOtherTasks)
                    if (plan.DailyPlanOtherTasks.All(x => x.Id != task.Id))
                        _context.Entry(task).State = EntityState.Deleted;
            }


            _context.DailyPlans.Update(plan);
        }
        else
        {
            _context.DailyPlans.Attach(plan);
        }

        await _context.SaveChangesAsync();
    }

    public async Task DeletePlan(Guid planId)
    {
        var plan = await _context.DailyPlans.FindAsync(planId);
        if (plan != null)
        {
            _context.Entry(plan).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
}