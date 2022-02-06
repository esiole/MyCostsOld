namespace MyCosts.API.Data.Repositories;

public interface IRepository<T> where T : class 
{
    void Add(T item);
    List<T> Get();
    void Remove(int id);
    void Update(T item);
}
