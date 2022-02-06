namespace MyCosts.API.Data.Repositories;

public interface IRepository<T> where T : class 
{
    void Add(T item);
    List<T> Get();
    T? Get(int id);
    void Remove(T item);
    void Update(T item);
}
