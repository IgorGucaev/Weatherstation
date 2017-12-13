using Station.Common.Exception;
using Station.Common.Interfaces;
using Station.Kernel.Infrastructure.Contracts;
using Station.Kernel.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;

namespace Station.Kernel.Infrastructure
{
    public class KernelContextUnitOfWork<TDbContext> : KernelUnitOfWork<TDbContext>, IKernelUnitOfWork
            where TDbContext : KernelDbContext, new()
    {
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

        public KernelContextUnitOfWork()
        {
            _repositories = new Dictionary<Type, IRepository>();
        }

        protected override ICollection<TDbContext> GetAllContexts()
        { return new TDbContext[] { this.Context }; }

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
