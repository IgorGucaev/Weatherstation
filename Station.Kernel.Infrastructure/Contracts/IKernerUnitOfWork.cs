using Station.Common.Contracts;
using Station.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Station.Kernel.Infrastructure.Contracts
{
    public interface IKernelUnitOfWork : ICommonUnitOfWork
    {
        T GetRepository<T>() where T : IRepository;

        //Activity FindActivity(object activityKey);
        //Activity CreateActivity(Activity activity);


        //FileBlob FindFileBlob(object key);
        //Avatar FindAvatar(object key);

        //UserReference FindUser(object userKey);
        //ManagerReference FindManager(object managerKey);
        //ManagerReference FindBubot();
        //OrganizationReference FindOrganization(object organizationKey);
        //CustomerReference FindCustomer(object customerKey);
        //DebtorReference FindDebtor(object debtorKey);
        //StaffReference FindStaff(object staffKey);

        //ICollection<ManagerReference> GetManagers(string groupName = "", bool includeDeleted = false);


        //ICollection<T> GetEntities<T>(Expression<Func<T, bool>> filter = null, params Expression<Func<T, object>>[] includes) where T : IdentifiableEntity<int>;
        //ICollection<T> GetTypes<T>(params Expression<Func<T, object>>[] includes) where T : Types<int>;
        //T GetType<T>(object key, params Expression<Func<T, object>>[] includes) where T : Types<int>;


        void SaveChanges();

        //void TransactionBegin();
        void TransactionBegin(System.Data.IsolationLevel isolationLevel = System.Data.IsolationLevel.Serializable);
        void TransactionCommit();
        void TransactionRollback();
    }
}
