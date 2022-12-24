using System.Data.Common;
using DataLayer;
using DataLayer.Models;
using EFHelpers;
using Microsoft.EntityFrameworkCore;

namespace People.Repository;

public class PeopleRepository
{
    private readonly NewbridgeContext _context;

    public PeopleRepository(NewbridgeContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Person>> GetPeople(int? plantId=null, int? valueStreamId=null, int? deptId=null)
    {
        IQueryable<Person> query = _context.People
            .Include(x => x.Departments)
            .ThenInclude(x => x.ValueStream)
            .AsNoTracking();

        if (plantId != null)
            query = query.Where(x => x.Departments.Any(z => z.ValueStream.PlantId == plantId));

        if (valueStreamId != null)
            query = query.Where(x => x.Departments.Any(z => z.ValueStreamId == valueStreamId));

        if (deptId != null)
            query = query.Where(x => x.Departments.Any(z => z.Id == deptId));

        return await query.ToListAsync();
    }

    public async Task<int> SavePerson(Person person)
    {
        if (person.Id > 0)
        {
            var dbPerson = await _context.People.Include(x=>x.Departments).FirstAsync(x => x.Id == person.Id);
            var toRemove = new List<Department>();
            foreach (var link in dbPerson.Departments)
            {
                if (person.Departments.Any(x => x.Id == link.Id))
                    person.Departments.RemoveAll(x => x.Id == link.Id);
                else
                    toRemove.Add(link);
            }

            foreach (var d in toRemove)
                dbPerson.Departments.Remove(d);
            
            
            dbPerson.FirstName = person.FirstName;
            dbPerson.LastName = person.LastName;
            dbPerson.EmailAddress = person.EmailAddress;
            dbPerson.IsRealPerson = person.IsRealPerson;
            
            foreach(var d in person.Departments)
                dbPerson.Departments.Add(d);
            
            await _context.SaveChangesAsync();
            return dbPerson.Id;
        }
        else
        {
            _context.People.Update(person);
            await _context.SaveChangesAsync();
            return person.Id;
        }
        
        
    }

    public async Task DeletePerson(int personId)
    {
        var person = await _context.People.FirstOrDefaultAsync(x => x.Id == personId);
        if (person != null)
        {
            _context.Entry(person).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

    }

    public async Task<Person> GetPerson(int id)
    {
        return await _context.People
            .AsNoTracking()
            .Include(x=>x.Departments)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}