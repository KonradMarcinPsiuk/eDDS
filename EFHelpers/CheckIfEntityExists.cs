using Microsoft.EntityFrameworkCore;

namespace EFHelpers;

public static class CheckIfEntityExists<T> where T : class
{
    public static async Task<bool> CheckIfExist(DbSet<T> dbSet, Guid obj)
    {
        return await dbSet.FindAsync(obj) != null;
    }

    public static async Task<bool> CheckIfExist(DbSet<T> dbSet, int obj)
    {
        return await dbSet.FindAsync(obj) != null;
    }
}