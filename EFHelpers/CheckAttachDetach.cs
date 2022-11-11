using Microsoft.EntityFrameworkCore;

namespace EFHelpers;

public static class CheckAttachDetach
{
    public static async Task Execute<T>(DbContext context, DbSet<T> set, int entityId, T entity) where T : class
    {
        var check = await set.FindAsync(entityId);

        if (check == null)
            set.Attach(entity).State = EntityState.Added;
        else
            context.Entry(check).State = EntityState.Detached;
    }
    
    public static async Task Execute<T>(DbContext context, DbSet<T> set, Guid entityId, T entity) where T : class
    {
        var check = await set.FindAsync(entityId);

        if (check == null)
            set.Attach(entity).State = EntityState.Added;
        else
            context.Entry(check).State = EntityState.Detached;
    }
}