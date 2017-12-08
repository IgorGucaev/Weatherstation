using Station.Common.Interfaces;

namespace Station.Common.Infrastructure.Contracts
{
    public interface ICommonRepository<TEntity> : IRepository<TEntity>
     where TEntity : class
    {
    }
}
