using System.Collections.Generic;

namespace Station.Common.Interfaces
{
    public interface IRepository
    { }

    public interface IRepository<T> : IRepository
        where T : class
    {
        IEnumerable<T> GetAll();

        T Get(object id);

        IEnumerable<T> Add(IEnumerable<T> entities);

        T Add(T entity);

        T Update(T entity);

        T Modify(T entity);

        void Delete(T entity);

        void Delete(object id);

        void Refresh(T entity);
    }
}
