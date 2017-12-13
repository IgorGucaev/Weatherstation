using System;
using System.Data;
using System.Collections.Generic;
using Station.Common.Interfaces;
using Station.Kernel.Infrastructure.Contracts;
using Station.Kernel.Infrastructure.Data;

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