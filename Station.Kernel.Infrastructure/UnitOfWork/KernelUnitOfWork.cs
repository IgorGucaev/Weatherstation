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
