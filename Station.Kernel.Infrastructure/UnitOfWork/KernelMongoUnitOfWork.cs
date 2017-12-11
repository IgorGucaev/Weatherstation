﻿using Station.Common.Interfaces;
using Station.Kernel.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Station.Kernel.Infrastructure.Data;
using MongoDB.Driver;
using Station.Common.Enums;

namespace Station.Kernel.Infrastructure
{
    public class KernelMongoUnitOfWork<TClient> : KernelUnitOfWork<TClient>, IKernelUnitOfWork
        where TClient : KernelMongoClient, new()
    {
        protected TClient _client;

        protected TClient Client
        {
            get
            {
                if (_client == null)
                    _client = new TClient();

                return _client;
            }
        }

        public KernelMongoUnitOfWork()
        {
            _repositories = new Dictionary<Type, IRepository>();
        }

        protected override ICollection<TClient> GetAllContexts()
        { return new TClient[] { this.Client }; }

        public virtual void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public virtual void Dispose()
        {
            
        }

        //public virtual T GetRepository<T>() where T : IRepository
        //{
        //    var interfaceType = typeof(T);
        //    if (_repositories.ContainsKey(interfaceType))
        //        return (T)_repositories[interfaceType];

        //    var repositoryTypes = this.GetType().Assembly.GetTypes().Where(t => interfaceType.IsAssignableFrom(t));
        //    Type repositoryType = null;

        //    // Если у меня несколько реализаций интерфейса репозитория, то нужный надо искать по custom attribute
        //    // Идея такая- uow и репозитории помечаются атрибутом DBAttribute. Какой у uow, такой должен быть и у репозитория
        //    if (repositoryTypes.Count() > 1)
        //    {
        //        var dbType = DBType.Unknown;
        //        var uofCustomAttributes = this.GetType().GetCustomAttributes(false);
        //        foreach (object uofAttrib in uofCustomAttributes)
        //        {
        //            if ((uofAttrib is Station.Common.Classes.DBAttribute))
        //            {
        //                dbType = ((Station.Common.Classes.DBAttribute)uofAttrib).Type;
        //                break;
        //            }
        //        }

        //        foreach (var t in repositoryTypes)
        //        {
        //            var customAttributes = t.GetCustomAttributes(true);
        //            foreach (object attrib in customAttributes)
        //            {
        //                if ((attrib is Station.Common.Classes.DBAttribute))
        //                {
        //                    var type = ((Station.Common.Classes.DBAttribute)attrib).Type;

        //                    if (type == dbType)
        //                    {
        //                        repositoryType = t;
        //                        break;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    else
        //        repositoryType = repositoryTypes.First();

        //    if (repositoryType == null)
        //        repositoryType = typeof(KernelMongoUnitOfWork<>).Assembly.GetTypes().SingleOrDefault(t => interfaceType.IsAssignableFrom(t));

        //    if (repositoryType == null)
        //        return default(T);

        //    T repository = default(T);

        //    if (!repositoryType.BaseType.IsGenericType)
        //    {
        //        // пытаемся подобрать нужный контекст для конструктора репозитория
        //        foreach (var context in this.GetAllContexts())
        //        {
        //            var constructor = repositoryType.GetConstructor(new Type[] { context.GetType() });
        //            if (constructor != null)
        //            {
        //                repository = (T)constructor.Invoke(new object[] { context });
        //                break;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        // тип контекста в определении Generica базового класса
        //        var repositoryContextType = repositoryType.BaseType.GetGenericArguments()[0];
        //        // конструктор репозитория от его типа контекста
        //        var constructor = repositoryType.GetConstructor(new Type[] { repositoryContextType });
        //        if (constructor != null)
        //        {
        //            // пытаемся подобрать нужный контекст для указанного типа контекста репозитория
        //            foreach (var context in this.GetAllContexts())
        //            {
        //                if (repositoryContextType.IsAssignableFrom(context.GetType()))
        //                {
        //                    repository = (T)constructor.Invoke(new object[] { context });
        //                    break;
        //                }
        //            }
        //        }
        //    }

        //    // попытка создать репозиторий из пустого конструктора
        //    if (repository == null)
        //    {
        //        var defaultConstructor = repositoryType.GetConstructor(Type.EmptyTypes);
        //        if (defaultConstructor != null)
        //            repository = (T)defaultConstructor.Invoke(null);
        //    }

        //    // если удалось создать репозиторий -> записываем его в коллекцию
        //    if (repository != null)
        //        _repositories.Add(interfaceType, repository);

        //    return repository;
        //}

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