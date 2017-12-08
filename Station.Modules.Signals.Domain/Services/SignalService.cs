using Station.Modules.Signals.Domain.Contracts;
using Station.Modules.Signals.Domain.Contracts.Repositories;
using Station.Modules.Signals.Domain.Contracts.Services;
using Station.Modules.Signals.Domain.Entities;
using System.Collections.Generic;

namespace Station.Modules.Signals.Domain.Services
{
    public class SignalService : MainService, ISignalService
    {
        public const int TRANSACTION_MINUTES = 5;

        //protected IOperationService _operationService;


        protected ISignalRepository SignalRepository
        { get { return this.UnitOfWork?.GetRepository<ISignalRepository>(); } }


        public SignalService(IStationUnitOfWork uow//,
                                            //IOperationService operationService,
          )
            : base(uow)
        {
            // _operationService = operationService;
        }

        public Signal Get(long Id)
        {
            //var manager = this.CurrentManager;
            //if (manager == null)
            //    throw new CustomAuthenticationException("Только авторизированным менеджерам доступна информация по Заявкам!");

            //if (includeShipmentOperations)
            //    return this.RequestRepository.GetWithOperations(requestID);
            //else
                return this.SignalRepository.GetByID(Id);
        }

        public List<Signal> GetByContract(long ContractId)
        {
            //var manager = this.CurrentManager;
            //if (manager == null)
            //    throw new CustomAuthenticationException("Только авторизированным менеджерам доступна информация по Заявкам!");

            //if (includeShipmentOperations)
            //    return this.RequestRepository.GetWithOperations(requestID);
            //else
            return this.SignalRepository.GetByContractId(ContractId);
        }
    }
}
