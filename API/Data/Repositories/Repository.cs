namespace MyCosts.API.Data.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly MyCostsDbContext _context;

    public Repository(MyCostsDbContext context)
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

    public virtual T Get(int id)
    {
        throw new NotImplementedException();
    }

    public virtual List<T> Get()
    {
        throw new NotImplementedException();
    }


    public virtual void Remove(int id)
    {
        throw new NotImplementedException();
        //lock (_context.Locker)
        //{
        //    _context.Remove(item);
        //    _context.SaveChanges();
        //}
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
