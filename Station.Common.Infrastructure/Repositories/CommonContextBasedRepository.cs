using Station.Common.Entities;
using Station.Common.Infrastructure.Contracts;
using Station.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Station.Common.Infrastructure.Repositories
{
    public class CommonContextBasedRepository<TDbContext, TEntity, TKey> : ICommonRepository<TEntity>
          where TDbContext : DbContext, new()
          where TEntity : Entity<TKey>
    {
        public bool IsAutoSave { get; protected set; }
        public TDbContext DbContext { get; protected set; }
        public DbSet<TEntity> DbSet { get; protected set; }

        public CommonContextBasedRepository(TDbContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException("dbContext");

            this.IsAutoSave = false;

            this.DbContext = dbContext;
            this.DbSet = this.DbContext.Set<TEntity>();
        }

        public virtual IQueryable<TEntity> QueryAll
        { get { return this.DbSet.AsNoTracking(); } }
        public virtual IQueryable<TEntity> QuerySingle
        { get { return this.DbSet; } }


        #region IRepository<T>
        protected virtual int SaveChanges()
        {
            return DbContext.SaveChanges();
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return this.QueryAll.ToList();
        }

        public virtual TEntity Get(TKey id)
        {
            var result = this.GetEntityByKey(this.QuerySingle, id);

            return result;
        }

        public virtual IEnumerable<TEntity> Add(IEnumerable<TEntity> entities)
        {
            var result = new List<TEntity>();

            if (entities != null)
                foreach (var entity in entities)
                    result.Add(this.Add(entity));

            return result;
        }

        public virtual TEntity Add(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("Entity", "Can't add null Entity to DbContext");

            EntityEntry dbEntityEntry = this.DbContext.Entry(entity);

            if (dbEntityEntry.State != EntityState.Detached)
            {
                dbEntityEntry.State = EntityState.Added;
            }
            else
            {
                this.DbSet.Add(entity);
            }

            if (this.IsAutoSave)
                this.SaveChanges();

            return entity;
        }

        public virtual TEntity Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("Entity", "Can't update null Entity to DbContext");

            EntityEntry dbEntityEntry = this.DbContext.Entry(entity);

            if (dbEntityEntry.State == EntityState.Detached)
            {
                this.DbSet.Attach(entity);
            }

            dbEntityEntry.State = EntityState.Modified;

            if (this.IsAutoSave)
                this.SaveChanges();

            return entity;
        }

        public virtual TEntity Modify(TEntity entity)
        {
            if (!entity.ID.Equals(default(TKey)))
                return this.Update(entity);

            return this.Add(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("Entity", "Can't delete null Entity to DbContext");

            if (entity is IDeletable)
            {
                ((IDeletable)entity).MarkDeleted();
                this.Update(entity);
                return;
            }

            this.DeleteReferences(entity);

            EntityEntry dbEntityEntry = this.DbContext.Entry(entity);

            if (dbEntityEntry.State != EntityState.Deleted)
            {
                dbEntityEntry.State = EntityState.Deleted;
            }
            else
            {
                this.DbSet.Attach(entity);
                this.DbSet.Remove(entity);
            }

            if (this.IsAutoSave)
                this.SaveChanges();
        }

        public virtual void Delete(TKey id)
        {
            var entity = this.Get(id);

            // if not found - assume already deleted...
            if (entity == null) return;

            this.Delete(entity);
        }

        protected virtual void DeleteReferences(TEntity entity)
        {
        }
        //public virtual void Restore(TEntity entity)
        //{
        //    this.DbContext.Entry<TEntity>(entity).Reload();
        //}

        public virtual void Refresh(TEntity entity)
        {
            if (entity != null)
            {
                var entry = this.DbContext.Entry(entity);
                if (entry.State != EntityState.Detached && entry.State != EntityState.Added)
                    entry.Reload();
            }
        }

        #endregion

        #region Functions

        protected TEntity GetEntityByKey(IQueryable<TEntity> query, TKey key)
        {
            if (key == null)
                return null;

            if (key is TKey)
                return query.SingleOrDefault(this.BuildFindExpression("ID", key));
            else
            {
               /* В LV думаю это не нужно var code = key.ToString();
                if (!String.IsNullOrEmpty(code))
                {
                    if (typeof(ICoded).IsAssignableFrom(typeof(TEntity)))
                        return query.SingleOrDefault(this.BuildFindExpression("Code", code));
                    if (typeof(INamed).IsAssignableFrom(typeof(TEntity)))
                        return query.SingleOrDefault(this.BuildFindExpression("Name", code));
                    else if (typeof(IType).IsAssignableFrom(typeof(TEntity)))
                        return query.SingleOrDefault(this.BuildFindExpression("Name", code));
                }*/
            }

            return null;
        }

  
        protected void LoadNavigation(TEntity entity, Expression<Func<TEntity, object>> navigation)
        {
            var entry = this.DbContext.Entry(entity);
            if (entity != null && navigation != null)
                entry.Reference(navigation).Load();
        }

        protected void LoadCollection(TEntity entity, Expression<Func<TEntity, IEnumerable<object>>> collection)
        {
            var entry = this.DbContext.Entry(entity);
            if (entity != null && collection != null)
                entry.Collection(collection).Load();
        }

        /// <summary>
        /// Builds Lambda Expression for IQuerable&lt;T&gt; (DbSet) to Find the entity by Field and Value
        /// </summary>
        /// <param name="field">Name of the field to search</param>
        /// <param name="value">Value of the field to search</param>
        /// <returns>Lambda Expression to make a query</returns>
        protected Expression<Func<TEntity, bool>> BuildFindExpression(string field, object value)
        {
            var entityExpression = Expression.Parameter(typeof(TEntity));

            var fieldExpression = Expression.Equal(Expression.Property(entityExpression, field), Expression.Constant(value));

            return Expression.Lambda<Func<TEntity, bool>>(fieldExpression, entityExpression);
        }

        public TEntity Get(object id)
        {
            throw new NotImplementedException();
        }

        public void Delete(object id)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
