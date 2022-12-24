using DataLayer.Models;
using EFHelpers;
using Microsoft.EntityFrameworkCore;

namespace TechnicalAssessment.Repository
{
    public class TechnicalAssessmentRepository
    {
        private NewbridgeContext _newbridgeContext;

        public TechnicalAssessmentRepository(NewbridgeContext newbridgeContext)
        {
            _newbridgeContext = newbridgeContext;
        }

        public async Task<int> SaveTransformation(Transformation transformation)
        {

            await CheckAttachDetach.Execute(_newbridgeContext, _newbridgeContext.LineAreas, transformation.LineAreaId, transformation.LineArea);

            foreach (var component in transformation.Components)
            {
                foreach (var action in component.ComponentActions)
                {
                    action.LineArea = null;
                    action.Owner = null;
                    if (action.Id == Guid.Empty)
                    {
                        action.Id = Guid.NewGuid();
                        _newbridgeContext.ComponentActions.Add(action);

                    }
                }
            }
            transformation.LineArea = null;
            _newbridgeContext.Transformations.Update(transformation);
            await _newbridgeContext.SaveChangesAsync();
            return transformation.Id;
        }

        public async Task<Transformation?> GetTransformation(int transformationId)
        {
            return await _newbridgeContext.Transformations
                .AsNoTracking()
                .Include(x => x.LineArea)
                .Include(x => x.Components)
                .ThenInclude(x => x.ComponentActions)
                .ThenInclude(x => x.Owner)
                .FirstOrDefaultAsync(x => x.Id == transformationId);
        }

        public async Task<IEnumerable<Transformation>> GetTransformations(int? lineAreaId = null)
        {
            var transformationQuery = _newbridgeContext.Transformations
                .AsNoTracking();

            if (lineAreaId != null)
                transformationQuery = transformationQuery.Where(x => x.LineAreaId == lineAreaId);

            return await transformationQuery
                .Include(x => x.LineArea)
                .Include(x => x.Components)
                .ThenInclude(x => x.ComponentActions)
                .ThenInclude(x => x.Owner)
                .ToListAsync();
        }

        public async Task DeleteAction(Guid actionId)
        {
            var action = await _newbridgeContext.ComponentActions.FindAsync(actionId);
            _newbridgeContext.Entry(action).State = EntityState.Deleted;
            await _newbridgeContext.SaveChangesAsync();
        }

        public async Task DeleteComponent(int componentId)
        {
            var component = await _newbridgeContext.Components.FindAsync(componentId);
            _newbridgeContext.Entry(component).State = EntityState.Deleted;
            await _newbridgeContext.SaveChangesAsync();
        }

        public async Task DeleteTransformation(int transformationId)
        {
            var transformation = await _newbridgeContext.Transformations.FindAsync(transformationId);
            _newbridgeContext.Entry(transformation).State = EntityState.Deleted;
            await _newbridgeContext.SaveChangesAsync();
        }

    }
}