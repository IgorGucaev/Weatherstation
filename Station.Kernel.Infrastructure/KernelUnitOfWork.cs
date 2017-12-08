using Station.Common.Exception;
using Station.Common.Interfaces;
using Station.Kernel.Infrastructure.Contracts;
using Station.Kernel.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Station.Kernel.Infrastructure
{
    public class KernelUnitOfWork<TDbContext> : IKernelUnitOfWork
            where TDbContext : KernelDbContext, new()
    {
        protected Dictionary<Type, IRepository> _repositories;
        protected List<IDbContextTransaction> _transactions;

        protected TDbContext _dbContext;

        protected TDbContext Context
        {
            get
            {
                if (_dbContext == null)
                    _dbContext = new TDbContext();

                return _dbContext;
            }
        }

        public KernelUnitOfWork()
        {
            _repositories = new Dictionary<Type, IRepository>();
        }

        protected virtual ICollection<DbContext> GetAllContexts()
        { return new DbContext[] { this.Context }; }

        public virtual void SaveChanges()
        {
            foreach (var context in this.GetAllContexts())
                context.SaveChanges();
        }

        public virtual void Dispose()
        {
            foreach (var context in this.GetAllContexts())
                context.Dispose();
        }

        public virtual T GetRepository<T>() where T : IRepository
        {
            var interfaceType = typeof(T);
            if (_repositories.ContainsKey(interfaceType))
                return (T)_repositories[interfaceType];

            var repositoryType = this.GetType().Assembly.GetTypes().SingleOrDefault(t => interfaceType.IsAssignableFrom(t));
            if (repositoryType == null)
                repositoryType = typeof(KernelUnitOfWork<>).Assembly.GetTypes().SingleOrDefault(t => interfaceType.IsAssignableFrom(t));

            if (repositoryType == null)
                return default(T);

            T repository = default(T);

            if (!repositoryType.BaseType.IsGenericType)
            {
                // пытаемся подобрать нужный контекст для конструктора репозитория
                foreach (var context in this.GetAllContexts())
                {
                    var constructor = repositoryType.GetConstructor(new Type[] { context.GetType() });
                    if (constructor != null)
                    {
                        repository = (T)constructor.Invoke(new object[] { context });
                        break;
                    }
                }
            }
            else
            {
                // тип контекста в определении Generica базового класса
                var repositoryContextType = repositoryType.BaseType.GetGenericArguments()[0];
                // конструктор репозитория от его типа контекста
                var constructor = repositoryType.GetConstructor(new Type[] { repositoryContextType });
                if (constructor != null)
                {
                    // пытаемся подобрать нужный контекст для указанного типа контекста репозитория
                    foreach (var context in this.GetAllContexts())
                    {
                        if (repositoryContextType.IsAssignableFrom(context.GetType()))
                        {
                            repository = (T)constructor.Invoke(new object[] { context });
                            break;
                        }
                    }
                }
            }

            // попытка создать репозиторий из пустого конструктора
            if (repository == null)
            {
                var defaultConstructor = repositoryType.GetConstructor(Type.EmptyTypes);
                if (defaultConstructor != null)
                    repository = (T)defaultConstructor.Invoke(null);
            }

            // если удалось создать репозиторий -> записываем его в коллекцию
            if (repository != null)
                _repositories.Add(interfaceType, repository);

            return repository;
        }

        /*

        public Activity CreateActivity(Activity activity)
        {
            return this.Context.Set<Activity>().Add(activity);
        }

        public Activity FindActivity(object activityKey)
        {
            if (activityKey == null)
                return null;

            var search = activityKey;

            if (search is Guid)
            {
                return this.Context.Set<Activity>()
                    .SingleOrDefault(t => t.ID == (Guid)search);
            }
            else if (search is String)
            {
                var id = Guid.Parse(search.ToString());

                return this.Context.Set<Activity>()
                    .SingleOrDefault(t => t.ID == id);
            }
            return null;
        }

        public FileBlob FindFileBlob(object key)
        {
            FileBlob blob = null;

            if (key != null)
            {
                if (key is int)
                    blob = this.Context.Set<FileBlob>().Find((int)key);
                else
                    blob = this.Context.Set<FileBlob>().SingleOrDefault(b => b.Code == key.ToString());
            }

            return blob;
        }

        public Avatar FindAvatar(object key)
        {
            Avatar avatar = null;

            if (key != null)
            {
                if (key is int)
                    avatar = this.Context.Set<Avatar>().Find((int)key);
                else
                {
                    var number = CommonService.IsNumber(key.ToString());
                    if (number != null && number.HasValue)
                        avatar = this.Context.Set<Avatar>().Find((int)number.Value);
                    else
                        avatar = this.Context.Set<Avatar>().SingleOrDefault(b => b.Code == key.ToString());
                }
            }

            return avatar;
        }

        public UserReference FindUser(object userKey)
        {
            if (userKey == null)
                return null;

            var search = userKey;

            if (search is int)
                return this.Context.Set<UserReference>()
                    .Find((int)search);
            else
                return this.Context.Set<UserReference>().SingleOrDefault(t => t.Login.Equals(search.ToString()));
        }

        public ManagerReference FindManager(object managerKey)
        {
            if (managerKey == null)
                return null;

            var search = managerKey;

            if (search is int)
                return this.Context.Set<ManagerReference>().Find((int)search);
            else
                return this.Context.Set<ManagerReference>().SingleOrDefault(t => t.Login.Equals(search.ToString()));
        }

        public ManagerReference FindBubot()
        {
            return this.FindManager(MetibData.BUBOT_ID);
        }

        public OrganizationReference FindOrganization(object organizationKey)
        {
            if (organizationKey == null)
                return null;

            var search = organizationKey;

            if (search is int)
                return this.Context.Set<OrganizationReference>().Find((int)search);
            else
                return this.Context.Set<OrganizationReference>().SingleOrDefault(t => t.Inn.Equals(search.ToString()));
        }

        public CustomerReference FindCustomer(object customerKey)
        {
            if (customerKey == null)
                return null;

            var search = customerKey;

            if (search is int)
                return this.Context.Set<CustomerReference>().Find((int)search);
            else
                return this.Context.Set<CustomerReference>().SingleOrDefault(t => t.Inn.Equals(search.ToString()));
        }

        public DebtorReference FindDebtor(object debtorKey)
        {
            if (debtorKey == null)
                return null;

            var search = debtorKey;

            if (search is int)
                return this.Context.Set<DebtorReference>().Find((int)search);
            else
                return this.Context.Set<DebtorReference>().SingleOrDefault(t => t.Inn.Equals(search.ToString()));
        }

        public StaffReference FindStaff(object staffKey)
        {
            if (staffKey == null)
                return null;

            var search = staffKey;

            if (search is int)
                return this.Context.Set<StaffReference>()
                    .Find((int)search);

            return null;
        }

        public ICollection<ManagerReference> GetManagers(string groupName = "", bool includeDeleted = false)
        {
            var result = this.Context.Set<ManagerReference>()
                .ToList();

            if (!string.IsNullOrEmpty(groupName))
                result = result.Where(m => m.GroupName.Equals(groupName, CommonService.StringComparison)).ToList();

            if (!includeDeleted)
                result = result.Where(m => !m.Deleted).ToList();

            return result;
        }


        public ICollection<T> GetEntities<T>(Expression<Func<T, bool>> filter = null, params Expression<Func<T, object>>[] includes)
            where T : IdentifiableEntity<int>
        {
            var query = this.Context.Set<T>().AsQueryable();

            foreach (var include in includes)
                query = query.Include(include);

            if (filter != null)
                query = query.Where(filter);

            return query.ToList();
        }

        public T GetType<T>(object key, params Expression<Func<T, object>>[] includes)
            where T : Types<int>
        {
            var query = this.Context.Set<T>().AsQueryable();
            foreach (var include in includes)
                query = query.Include(include);

            // если тип не задан - вернуть по умолчанию
            if (key == null || string.IsNullOrWhiteSpace(key.ToString()))
                return query.FirstOrDefault();

            var search = key;

            // если передан ID - найти по ID
            if (search is int)
                return query.FirstOrDefault(t => t.ID == (int)search);
            else
                return query.FirstOrDefault(t => t.Identifier.Equals(search.ToString()));
        }

        public ICollection<T> GetTypes<T>(params Expression<Func<T, object>>[] includes)
            where T : Types<int>
        {
            var query = this.Context.Set<T>().AsQueryable();
            foreach (var include in includes)
                query = query.Include(include);

            return query.ToList();
        }

        //public void TransactionBegin()
        //{
        //    _transactions = new List<DbContextTransaction>();
        //    foreach (var context in this.GetAllContexts())
        //    {
        //        var transaction = context.Database.BeginTransaction();
        //        _transactions.Add(transaction);
        //    }
        //}
        */

        public void TransactionBegin(IsolationLevel isolationLevel = IsolationLevel.Serializable)
        {
            if (_transactions != null && _transactions.Count > 0)
                throw new CustomOperationException("Внимание, уже существуют созданые транзакции на данном контексте! Нельзя задублировать транзакции!");

            _transactions = new List<IDbContextTransaction>();
            foreach (var context in this.GetAllContexts())
            {
                var transaction = context.Database.BeginTransaction(isolationLevel);
                _transactions.Add(transaction);
            }
        }

        public void TransactionCommit()
        {
            foreach (var transaction in _transactions)
            {
                transaction.Commit();
                transaction.Dispose();
            }
            _transactions = null;
        }

        public void TransactionRollback()
        {
            foreach (var transaction in _transactions)
            {
                transaction.Rollback();
                transaction.Dispose();
            }
            _transactions = null;
        }
    }
}
