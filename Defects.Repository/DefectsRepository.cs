using DataLayer;
using DataLayer.Models;
using DataLayer.Models.Enums;
using EFHelpers;
using Microsoft.EntityFrameworkCore;

namespace Defects.Repository;

public class DefectsRepository
{
    private readonly NewbridgeContext _defectsContext;

    public DefectsRepository(NewbridgeContext defectsContext)
    {
        _defectsContext = defectsContext;
    }

    public async Task<Defect?> GetDefect(Guid defectId)
    {
        return await _defectsContext.Defects
            .Include(x => x.LineArea)
            .Include(x => x.Owner)
            .FirstOrDefaultAsync(x => x.Id == defectId);
    }

    public async Task<IEnumerable<Defect>> GetDefects(int? lineId, bool? openOnly)
    {
        var query = _defectsContext.Defects
            .Include(x => x.LineArea)
            .Include(x => x.Owner)
            .AsNoTracking();

        if (lineId != null)
            query = query.Where(x => x.LineArea.ProductionLineId == lineId);

        if (openOnly != null)
            query = query.Where(x => x.Status != 3);

        return await query.ToListAsync();
    }
    
    public async Task<IEnumerable<Defect>> GetDefectsForLine(int lineId, string status)
    {
        var query = _defectsContext.Defects
            .Include(x => x.LineArea)
            .Include(x => x.Owner)
            .AsNoTracking();
        
        query = query.Where(x => x.LineArea.ProductionLineId == lineId);

        if (string.Equals(status.ToLower(),"open"))
            query = query.Where(x => x.Status!=2);

        if (string.Equals(status.ToLower(), "closed"))
            query = query.Where(x => x.Status==2);

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<Defect>> GetDefects(int? lineId, int? areaId, bool closedOnly)
    {
        var query = _defectsContext.Defects
            .Include(x => x.Owner)
            .Include(x => x.LineArea)
            .AsNoTracking();

        if (lineId != null)
            query = query.Where(x => x.LineArea.ProductionLineId == lineId);

        if (areaId != null)
            query = query.Where(x => x.LineAreaId == lineId);

        if (closedOnly)
            query = query.Where(x => x.Status == 3); //override original status
        else
            query = query.Where(x => x.Status != 3); 

        return await query.ToListAsync();
    }

    public async Task AttachDefect(Defect defect)
    {
        var check = await CheckIfEntityExists<Defect>.CheckIfExist(_defectsContext.Defects, defect.Id);
        _defectsContext.ChangeTracker.Clear();
        _defectsContext.Defects.Attach(defect).State = check ? EntityState.Modified : EntityState.Added;
    }

    public async Task MarkDefectAsClosed(Guid defectId)
    {
        var defect = await _defectsContext.Defects.FindAsync(defectId);
        if (defect != null) defect.Status = 3;
    }

    public async Task MarkDefectAsOpen(Guid defectId)
    {
        var defect = await _defectsContext.Defects.FindAsync(defectId);
        if (defect != null) defect.Status = 1;
    }

    public async Task SaveContext()
    {
        await _defectsContext.SaveChangesAsync();
    }

    public async Task DeleteDefect(Guid defectId)
    {
        var defect = await _defectsContext.Defects.Include(x=>x.DailyPlanDefectTasks).FirstOrDefaultAsync(x=>x.Id==defectId);
        if (defect != null)
        {
            foreach (var t in defect.DailyPlanDefectTasks)
            {
                _defectsContext.Entry(t).State = EntityState.Deleted;
            }
            _defectsContext.Entry(defect).State = EntityState.Deleted;
            await _defectsContext.SaveChangesAsync();
        }
    }
}