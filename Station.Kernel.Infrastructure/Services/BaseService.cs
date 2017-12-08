using System;

namespace Station.Kernel.Infrastructure.Services
{
    public class BaseService : IDisposable
    {
        public const string EXCEPTION_NOT_AUTHORIZED = "Пользователь не авторизован!";

        //protected ITokenProvider _tokenProvider;

        //public ITokenProvider TokenProvider { get { return _tokenProvider; } }

        //public MetibToken Token { get { return this.TokenProvider?.GetToken() as MetibToken; } }

        //public int CurrentUserID
        //{ get { return this.Token?.UserID ?? 0; } }

        //public int CurrentManagerID
        //{ get { return this.Token?.ManagerID ?? 0; } }

        //public int CurrentOrganizationID
        //{ get { return this.Token?.OrganizationID ?? 0; } }

        //public BaseService(ITokenProvider tokenProvider)
        //{
        //    _tokenProvider = tokenProvider;
        //}

        public BaseService()
        {
  
        }

        protected bool CheckRights(int userID, int organizationID, int customerID, int debtorID)
        {
            return true;// MetibToken.CheckRight(this.Token, userID, organizationID, customerID, debtorID);
        }

        public virtual void Dispose()
        {

        }
    }
}
