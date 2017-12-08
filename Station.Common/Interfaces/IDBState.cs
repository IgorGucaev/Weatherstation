using Station.Common.Enums;

namespace Station.Common.Interfaces
{
    public interface IDbState
    {
        DbState DbState { get; }

        void ChangeDbState(DbState state);
        void DeleteFromDb();
    }
}
