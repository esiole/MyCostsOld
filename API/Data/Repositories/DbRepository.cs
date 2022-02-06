namespace MyCosts.API.Data.Repositories;

public abstract class DbRepository<T> : IRepository<T> 
    where T : class
{
    protected readonly MyCostsDbContext _context;

    public DbRepository(MyCostsDbContext context)
    {
        _context = context;
    }

    public virtual void Add(T item)
    {
        lock (_context.Locker)
        {
            _context.Add(item);
            _context.SaveChanges();
        }
    }

    public abstract List<T> Get();
    public abstract T? Get(int id);

    public virtual void Remove(T item)
    {
        lock (_context.Locker)
        {
            _context.Remove(item);
            _context.SaveChanges();
        }
    }

    protected void Save()
    {
        lock (_context.Locker)
        {
            _context.SaveChanges();
        }
    }

    public virtual void Update(T item)
    {
        lock (_context.Locker)
        {
            _context.Update(item);
            _context.SaveChanges();
        }
    }
}
