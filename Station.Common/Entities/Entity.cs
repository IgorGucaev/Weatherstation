using Station.Common.Enums;
using Station.Common.Interfaces;

namespace Station.Common.Entities
{
    public abstract class Entity<T> : IdentifiableEntity<T>, IEntity, IDbState
    {
        protected DbState _dbState = DbState.None;

        DbState IDbState.DbState { get { return _dbState; } }

        void IDbState.ChangeDbState(DbState state)
        { _dbState = state; }

        void IDbState.DeleteFromDb()
        { _dbState = DbState.Deleted; }
    }
}
