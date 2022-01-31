namespace MyCosts.API.Data.Repositories;

public interface IRepository<T> where T : class
{
    void Add(T item);
    T Get(int id);
    List<T> Get();
    void Remove(int id);
    void Update(T item);
}
