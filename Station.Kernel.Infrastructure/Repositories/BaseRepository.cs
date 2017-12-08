using Station.Common.Entities;
using Station.Common.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Station.Kernel.Infrastructure.Repositories
{

    public class BaseRepository<TDbContext, TEntity, TKey> : CommonRepository<TDbContext, TEntity, TKey>
         where TDbContext : DbContext, new()
         where TEntity : Entity<TKey>
    {
        protected virtual IQueryable<TEntity> GetQuery(bool multiple, bool tracking = false)
        {
            IQueryable<TEntity> query = this.DbSet;

            if (!tracking)
                query = query.AsNoTracking();

            return query;
        }

        public override IQueryable<TEntity> QuerySingle
        { get { return this.GetQuery(false, true); } }

        public override IQueryable<TEntity> QueryAll
        { get { return this.GetQuery(true, false); } }

        public BaseRepository(TDbContext context)
            : base(context) { }

        public virtual TEntity GetTracking(TKey id)
        {
            var entity = base.GetEntityByKey(this.GetQuery(false, true), id);

            this.Refresh(entity);

            return entity;
        }
    }

    public class BaseModelRepository<TDbContext, TEntity, TKey> : BaseRepository<TDbContext, TEntity,TKey>
         where TDbContext : DbContext, new()
         where TEntity : Entity<TKey>
    {
        public override IQueryable<TEntity> QuerySingle
        { get { return this.GetQuery(false, false); } }

        public override IQueryable<TEntity> QueryAll
        { get { return this.GetQuery(true, false); } }

        public BaseModelRepository(TDbContext context)
            : base(context) { }
    }
}
