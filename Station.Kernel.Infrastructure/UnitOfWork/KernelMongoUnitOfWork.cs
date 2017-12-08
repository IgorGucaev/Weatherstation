using Station.Common.Interfaces;
using Station.Kernel.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using MongoDB.Bson;
using System.Linq;
using System.Data;
using Station.Common.Exception;
using Microsoft.EntityFrameworkCore.Storage;
using Station.Kernel.Infrastructure.Data;
using MongoDB.Driver;

namespace Station.Kernel.Infrastructure
{
    public class KernelMongoUnitOfWork<TDatabase> : IKernelUnitOfWork
            where TDatabase : KernelMongoDB, new()
    {
        protected Dictionary<Type, IRepository> _repositories;
        protected List<IDbContextTransaction> _transactions;

        protected TDatabase _database;

        protected TDatabase Database
        {
            get
            {
                if (_database == null)
                    _database = new TDatabase();

                return _database;
            }
        }

        public KernelMongoUnitOfWork()
        {
            _repositories = new Dictionary<Type, IRepository>();
        }

        protected virtual ICollection<TDatabase> GetAllContexts()
        { return new TDatabase[] { this.Database }; }

        public virtual void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public virtual void Dispose()
        {
            
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
            throw new NotImplementedException();
        }

        public void TransactionCommit()
        {
            throw new NotImplementedException();
        }

        public void TransactionRollback()
        {
            throw new NotImplementedException();
        }
    }
}