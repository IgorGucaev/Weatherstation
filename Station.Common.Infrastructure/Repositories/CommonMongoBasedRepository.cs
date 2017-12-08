using MongoDB.Bson;
using MongoDB.Driver;
using Station.Common.Entities;
using Station.Common.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Station.Common.Infrastructure.Repositories
{
    public class CommonMongoBasedRepository<TDatabase, TEntity, TKey> : ICommonRepository<TEntity>
        where TDatabase : IMongoDatabase
             where TEntity : Entity<TKey>
    {
        public TDatabase Db { get; protected set; }
        public IMongoClient Client { get; protected set; }
        public string CollectionName { get; protected set; }

        public CommonMongoBasedRepository(TDatabase database, string collectionName)
        {
            if (database == null)
                throw new ArgumentNullException("database not exist");

            this.Db = database;
            this.Client = database.Client;
            this.CollectionName = collectionName;
        }

        //public virtual IQueryable<TEntity> QueryAll
        //{ get { return this.DbSet.AsNoTracking(); } }
        //public virtual IQueryable<TEntity> QuerySingle
        //{ get { return this.DbSet; } }


        #region IRepository<T>

        public virtual IEnumerable<TEntity> GetAll()
        {
            return this.Db.GetCollection<TEntity>(this.CollectionName).AsQueryable();
        }

        public virtual TEntity Get(TKey id)
        {
            var collection = this.Db.GetCollection<TEntity>(this.CollectionName);
            var filter = new BsonDocument
            {
                {"_id", id.ToString()}
            };

            var cursor = collection.Find(filter);
            return cursor.FirstOrDefault();
        }

        public virtual IEnumerable<TEntity> Add(IEnumerable<TEntity> entities)
        {
            ////var result = new List<TEntity>();

            ////if (entities != null)
            ////    foreach (var entity in entities)
            ////        result.Add(this.Add(entity));

            return null;// result;
        }

        public virtual TEntity Add(TEntity entity)
        {
            ////if (entity == null)
            ////    throw new ArgumentNullException("Entity", "Can't add null Entity");

            ////var collection = database.GetCollection<Person>("people");
            ////Person person1 = new Person
            ////{
            ////    Name = "Jack",
            ////    Age = 29,
            ////    Languages = new List<string> { "english", "german" },
            ////    Company = new Company
            ////    {
            ////        Name = "Google"
            ////    }
            ////};
            ////await collection.InsertOneAsync(person1);

            return entity;
        }

        public virtual TEntity Update(TEntity entity)
        {
            ////if (entity == null)
            ////    throw new ArgumentNullException("Entity", "Can't update null Entity to DbContext");

            ////EntityEntry dbEntityEntry = this.DbContext.Entry(entity);

            ////if (dbEntityEntry.State == EntityState.Detached)
            ////{
            ////    this.DbSet.Attach(entity);
            ////}

            ////dbEntityEntry.State = EntityState.Modified;

            ////if (this.IsAutoSave)
            ////    this.SaveChanges();

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
            ////if (entity == null)
            ////    throw new ArgumentNullException("Entity", "Can't delete null Entity to DbContext");

            ////if (entity is IDeletable)
            ////{
            ////    ((IDeletable)entity).MarkDeleted();
            ////    this.Update(entity);
            ////    return;
            ////}

            ////this.DeleteReferences(entity);

            ////EntityEntry dbEntityEntry = this.DbContext.Entry(entity);

            ////if (dbEntityEntry.State != EntityState.Deleted)
            ////{
            ////    dbEntityEntry.State = EntityState.Deleted;
            ////}
            ////else
            ////{
            ////    this.DbSet.Attach(entity);
            ////    this.DbSet.Remove(entity);
            ////}

            ////if (this.IsAutoSave)
            ////    this.SaveChanges();
        }

        public virtual void Delete(TKey id)
        {
            ////var entity = this.Get(id);

            ////// if not found - assume already deleted...
            ////if (entity == null) return;

            ////this.Delete(entity);
        }
        #endregion

        #region Functions
        public TEntity Get(object id)
        {
            throw new NotImplementedException();
        }

        public void Delete(object id)
        {
            throw new NotImplementedException();
        }

        public void Refresh(TEntity entity)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}