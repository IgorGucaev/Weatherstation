using Station.Kernel.Infrastructure.Contracts;

namespace Station.Kernel.Infrastructure.Services
{
    public abstract class KernelService<TUnitOfWork> : BaseService, IKernelService<TUnitOfWork>
           where TUnitOfWork : IKernelUnitOfWork
    {
        public const string EXCEPTION_NOT_USER = "Текущий пользователь не авторизован или отсутствует в базе!";
        public const string EXCEPTION_NOT_MANAGER = "Текущий менеджер не авторизован или отсутствует в базе!";
        public const string EXCEPTION_NOT_ADMIN = "Данное действие доступно исключительно администраторам!";
        public const string EXCEPTION_NOT_PERMISSIONS = "Недостаточно полномочий для выполнения данной операции!";

        protected TUnitOfWork _uow;

        ////protected UserReference _user;
        ////protected ManagerReference _manager;
        ////protected OrganizationReference _organization;

        public TUnitOfWork UnitOfWork { get { return _uow; } }

        ////public UserReference CurrentUser
        ////{
        ////    get
        ////    {
        ////        if ((_user?.ID ?? 0) != this.CurrentUserID && this.CurrentUserID > 0)
        ////            _user = _uow.FindUser(this.CurrentUserID);

        ////        if (_user == null || _user.ID <= 0)
        ////            return null;
        ////        //throw new CustomNotFoundException(EXCEPTION_NOT_USER);

        ////        return _user;
        ////    }
        ////}

        ////public ManagerReference CurrentManager
        ////{
        ////    get
        ////    {
        ////        if (_manager == null && this.CurrentManagerID > 0)
        ////            _manager = _uow.FindManager(this.CurrentManagerID);

        ////        if (_manager == null || _manager.ID <= 0)
        ////            return null;
        ////        //throw new CustomNotFoundException(EXCEPTION_NOT_MANAGER);

        ////        return _manager;
        ////    }
        ////}

        ////public OrganizationReference CurrentOrganization
        ////{
        ////    get
        ////    {
        ////        if (_organization == null && this.CurrentOrganizationID > 0)
        ////            _organization = _uow.FindOrganization(this.CurrentOrganizationID);

        ////        if (_organization == null || _organization.ID <= 0)
        ////            throw new CustomNotFoundException($"Текущая организация не найдена в базе!");

        ////        return _organization;
        ////    }
        ////}

        public KernelService(TUnitOfWork uow)
            : base()
        {
            _uow = uow;
        }

        public void SaveChanges()
        {
            this.UnitOfWork.SaveChanges();
        }

        public override void Dispose()
        {
            this.UnitOfWork.Dispose();
        }
    }
}
