using Station.Common.Entities;
using Station.Common.Interfaces;

namespace Station.Common.Contracts
{
    public interface ICommonRepository<TEntity, T> : IRepository<TEntity>
        where TEntity : Entity<T>
    {
       //// TEntity GetTracking(T id);
    }
}
