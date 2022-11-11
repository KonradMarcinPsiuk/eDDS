using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DailyResults.Repository
{
    public  class DailyResultsRepository
    {
        private NewbridgeContext _context;

        public DailyResultsRepository(NewbridgeContext context)
        {
            _context = context;
        }

        public async Task<DailyResult> GetResultForLineAndDate(DateTime date, int lineId)
        {
            var dailyResult = await _context.DailyResults.AsNoTracking()
                .Include(x=>x.ProductionLine)
                .FirstOrDefaultAsync(x =>
                x.Date.Day == date.Day && x.Date.Month == date.Month && x.Date.Year == date.Year &&
                x.ProductionLineId == lineId);

            if(dailyResult!=null) { return dailyResult; }

            dailyResult = new DailyResult();
            dailyResult.ProductionLineId = lineId;
            dailyResult.Date = new DateTime(date.Year, date.Month, date.Day);
            _context.DailyResults.Add(dailyResult);
            await _context.SaveChangesAsync();
            return dailyResult;
        }

        public async Task<IEnumerable<DailyResult>> GetResultsForDateAndDepartment(DateTime date, int deptId)
        {
            var results = await _context.DailyResults
                .AsNoTracking()
                .Include(x => x.ProductionLine)
                .Where(x => x.Date.Year == date.Year && x.Date.Month == date.Month && x.Date.Day == date.Day && x.ProductionLine.DepartmentId == deptId)
                .ToListAsync();

            return results;
        }

        public async Task<bool> CheckIfResultForLineAndDateExists(DateTime date, int lineId)
        {
            var dailyResult = await _context.DailyResults.AsNoTracking().FirstOrDefaultAsync(x =>
                x.Date.Day == date.Day && x.Date.Month == date.Month && x.Date.Year == date.Year &&
                x.ProductionLineId == lineId);

            return dailyResult != null;
        }

        public async Task<bool> SaveDailyResult(DailyResult result)
        {
            try
            {
                _context.DailyResults.Update(result);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}
