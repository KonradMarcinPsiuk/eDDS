using DataLayer.Interfaces;
using DataLayer.Models;
using EFHelpers;
using Microsoft.EntityFrameworkCore;


namespace PlannedStop.Repository;

public class PlannedStopRepository
{
    private readonly NewbridgeContext _plannedStopContext;

    public PlannedStopRepository(NewbridgeContext plannedStopContext)
    {
        _plannedStopContext = plannedStopContext;
    }

    public async Task<PmTask?> GetPmTask(Guid taskId)
    {
        return await _plannedStopContext.PmTasks.AsNoTracking().Include(x => x.LineArea).FirstAsync(x=>x.Id==taskId);
    }

    public async Task<CilTask?> GetCilTask(Guid taskId)
    {
        return await _plannedStopContext.CilTasks.AsNoTracking().Include(x => x.LineArea).FirstAsync(x => x.Id == taskId);
    }

    public async Task<ClTask?> GetClTask(Guid taskId)
    {
        return await _plannedStopContext.ClTasks.AsNoTracking().Include(x => x.LineArea).FirstAsync(x => x.Id == taskId);
    }

    public async Task<OtherTask?> GetOtherTask(Guid taskId)
    {
        return await _plannedStopContext.OtherTasks.AsNoTracking().Include(x => x.LineArea).FirstAsync(x => x.Id == taskId);
    }

    public async Task<IEnumerable<PmTask>> GetPmTasks(int lineId, bool openOnly)
    {
        var q = _plannedStopContext.PmTasks.AsNoTracking().Include(x => x.LineArea).Where(x => x.LineArea.ProductionLineId == lineId);
        if (openOnly) q = q.Where(x => x.Status != 2);
        return await q.ToListAsync();
    }

    public async Task<IEnumerable<CilTask>> GetCilTasks(int lineId, bool openOnly)
    {
        var q = _plannedStopContext.CilTasks.AsNoTracking().Include(x => x.LineArea).Where(x => x.LineArea.ProductionLineId == lineId);
        if (openOnly) q = q.Where(x => x.Status != 2);
        return await q.ToListAsync();
    }

    public async Task<IEnumerable<ClTask>> GetClTasks(int lineId, bool openOnly)
    {
        var q = _plannedStopContext.ClTasks.AsNoTracking().Include(x => x.LineArea).Where(x => x.LineArea.ProductionLineId == lineId);
        if (openOnly) q = q.Where(x => x.Status != 2);
        return await q.ToListAsync();
    }

    public async Task<IEnumerable<OtherTask>> GetOtherTasks(int lineId, bool openOnly)
    {
        var q = _plannedStopContext.OtherTasks.AsNoTracking().Include(x => x.LineArea).Where(x => x.LineArea.ProductionLineId == lineId);
        if (openOnly) q = q.Where(x => x.Status != 2);
        return await q.ToListAsync();
    }

    public async Task AttachPmTask(PmTask task)
    {
        await AttachTask(_plannedStopContext.PmTasks, task);
    }

    public async Task AttachCilTask(CilTask task)
    {
        await AttachTask(_plannedStopContext.CilTasks, task);
    }

    public async Task AttachClTask(ClTask task)
    {
        await AttachTask(_plannedStopContext.ClTasks, task);
    }

    public async Task AttachOtherTask(OtherTask task)
    {
        await AttachTask(_plannedStopContext.OtherTasks, task);
    }

    public async Task AttachPmTasks(IEnumerable<PmTask> tasks)
    {
        await AttachTasks(_plannedStopContext.PmTasks, tasks);
    }

    public async Task AttachCilTasks(IEnumerable<CilTask> tasks)
    {
        await AttachTasks(_plannedStopContext.CilTasks, tasks);
    }

    public async Task AttachClTasks(IEnumerable<ClTask> tasks)
    {
        await AttachTasks(_plannedStopContext.ClTasks, tasks);
    }

    public async Task AttachOtherTasks(IEnumerable<OtherTask> tasks)
    {
        await AttachTasks(_plannedStopContext.OtherTasks, tasks);
    }

    private async Task<T?> GetTask<T>(DbSet<T> dbSet, Guid taskId) where T : class
    {
        return await dbSet.FindAsync(taskId);
    }

    private async Task<IEnumerable<T>> GetTasks<T>(DbSet<T> dbSet, int? lineId, bool closedOnly) where T : class, ITask
    {
        var query = dbSet.AsNoTracking();

        if (lineId != null)
            query = query.Where(x => x.LineArea.ProductionLineId == lineId);

        if (closedOnly)
            query = query.Where(x => x.Status == 2);
        else
            query = query.Where(x => x.Status != 2);

        return await query.ToListAsync();
    }

    private async Task AttachTask<T>(DbSet<T> dbSet, T task) where T : class, ITask
    {
        var check = await CheckIfEntityExists<T>.CheckIfExist(dbSet, task.Id);
        dbSet.Attach(task).State = check ? EntityState.Modified : EntityState.Added;
    }

    private async Task AttachTasks<T>(DbSet<T> dbSet, IEnumerable<T> tasks) where T : class, ITask
    {
        foreach (var task in tasks)
        {
            var check = await CheckIfEntityExists<T>.CheckIfExist(dbSet, task.Id);
            dbSet.Attach(task).State = check ? EntityState.Modified : EntityState.Added;
        }
    }

    public async Task SaveContext()
    {
        await _plannedStopContext.SaveChangesAsync();
    }

    public async Task DeletePmTask(Guid taskId)
    {
        var task = await _plannedStopContext.PmTasks.Include(x => x.DailyPlanPmTasks).FirstOrDefaultAsync(x => x.Id == taskId);
        if (task != null)
        {
            foreach (var t in task.DailyPlanPmTasks)
            {
                _plannedStopContext.Entry(t).State = EntityState.Deleted;
            }
            _plannedStopContext.Entry(task).State = EntityState.Deleted;
            await _plannedStopContext.SaveChangesAsync();
        }
    }

    public async Task DeleteCilTask(Guid taskId)
    {
        var task = await _plannedStopContext.CilTasks.Include(x => x.DailyPlanCilTasks).FirstOrDefaultAsync(x => x.Id == taskId);
        if (task != null)
        {
            foreach (var t in task.DailyPlanCilTasks)
            {
                _plannedStopContext.Entry(t).State = EntityState.Deleted;
            }
            _plannedStopContext.Entry(task).State = EntityState.Deleted;
            await _plannedStopContext.SaveChangesAsync();
        }
    }

    public async Task DeleteClTask(Guid taskId)
    {
        var task = await _plannedStopContext.ClTasks.Include(x => x.DailyPlanClTasks).FirstOrDefaultAsync(x => x.Id == taskId);
        if (task != null)
        {
            foreach (var t in task.DailyPlanClTasks)
            {
                _plannedStopContext.Entry(t).State = EntityState.Deleted;
            }
            _plannedStopContext.Entry(task).State = EntityState.Deleted;
            await _plannedStopContext.SaveChangesAsync();
        }
    }

    public async Task DeleteOtherTask(Guid taskId)
    {
        var task = await _plannedStopContext.OtherTasks.Include(x => x.DailyPlanOtherTasks).FirstOrDefaultAsync(x => x.Id == taskId);
        if (task != null)
        {
            foreach (var t in task.DailyPlanOtherTasks)
            {
                _plannedStopContext.Entry(t).State = EntityState.Deleted;
            }
            _plannedStopContext.Entry(task).State = EntityState.Deleted;
            await _plannedStopContext.SaveChangesAsync();
        }
    }
}

