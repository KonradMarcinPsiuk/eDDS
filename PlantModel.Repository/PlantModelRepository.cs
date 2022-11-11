using DataLayer;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace PlantModel.Repository;

public class PlantModelRepository
{
    private readonly NewbridgeContext _context;

    public PlantModelRepository(NewbridgeContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Plant>> GetAllPlants()
    {
        return await _context.Plants.AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<ValueStream>> GetValueStreamsForPlant(int plantId)
    {
        return await _context.ValueStreams.AsNoTracking().Where(x => x.PlantId == plantId).ToListAsync();
    }

    public async Task<IEnumerable<Department>> GetDepartmentsForValueStream(int valueStreamId)
    {
        return await _context.Departments.AsNoTracking().Where(x => x.ValueStreamId == valueStreamId).ToListAsync();
    }

    public async Task<IEnumerable<Department>> GetDepartmentsForPlant(int plantId)
    {
        return await _context.Departments.AsNoTracking()
            .Include(x=>x.ValueStream)
            .Where(x => x.ValueStream.PlantId == plantId)
            .ToListAsync();
    }
    public async Task<IEnumerable<ProductionLine>> GetProductionLinesForDepartment(int departmentId)
    {
        return await _context.ProductionLines.Include(x => x.LineAreas).AsNoTracking()
            .Where(x => x.DepartmentId == departmentId).ToListAsync();
    }

    public async Task<IEnumerable<LineArea>> GetLineAreasForLine(int lineId)
    {
        return await _context.LineAreas.AsNoTracking().Where(x => x.ProductionLineId == lineId).ToListAsync();
    }

   
}