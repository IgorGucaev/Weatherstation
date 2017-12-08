using Station.Modules.Signals.Domain.Entities;

namespace Station.Modules.Signals.Domain.Contracts.Services
{
    public interface ISignalService
    {
        Signal Get(long id);
    }
}
