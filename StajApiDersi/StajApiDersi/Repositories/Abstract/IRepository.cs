using StajApiDersi.Models.Abstract;

namespace StajApiDersi.Repositories.Abstract
{
    public interface IRepository<T> where T :BaseEntity
    {
        bool Add(T item);
        bool Edit(T item);
        bool Remove(T item);
        List<T> GetAll();
        T GetById(int id);
    }
}
