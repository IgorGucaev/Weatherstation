using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Station.Common.Enums;
using Station.Common.Interfaces;
using Station.Common.Classes;

namespace Station.Common.Infrastructure.Data
{
    public class CommonDbContext : DbContext
    {
        public static Action<string, string> Log = null;

        protected const bool _proxyCreationEnabled = false;
        protected const bool _lazyLoadingEnabled = false;

        ////protected string _guid = CommonService.NewGuid;
        protected List<object> _objects = new List<object>();

        public CommonDbContext()
            : base()
        {

        }

        public CommonDbContext(DbContextOptions<CommonDbContext> options)
            : base(options)
        {
           
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(AppConfiguration.Configuration["SignalFromSql"]);
        }

        protected List<EntityEntry<T>> GetEntries<T>()
            where T : class
        {
            var result = this.ChangeTracker
                       .Entries<T>()
                       .ToList();

            // добавляем кастомные объекты этого типа
            result.AddRange(_objects.OfType<T>().Select(o => this.Entry(o)).ToList());

            return result;
        }

        protected void MatchDbState(IDbState obj)
        {
            if (obj == null || obj.DbState == DbState.None)
                return;

            this.Entry(obj).State = ConvertEntityState(obj.DbState);
        }

        protected void MatchDbStateForAllEnties()
        {
            // get DbStated Enties
            var entries = this.GetEntries<IDbState>()
                .Where(e => e.Entity.DbState != DbState.None)
                .ToList();

            // update entry State depends on DbState
            entries.ForEach(e => this.MatchDbState(e.Entity));
        }

        protected void ClearDbStateForAllEntries()
        {
            //this.ChangeTracker.Entries<IDbState>().ToList()
            this.GetEntries<IDbState>()
                .ForEach(e => e.Entity.ChangeDbState(DbState.None));
        }

   
        public override int SaveChanges()
        {
            this.MatchDbStateForAllEnties();

            // MAIN save changes
            var result = base.SaveChanges();

            // set back DbStates to None (after saving)
            this.ClearDbStateForAllEntries();

            return result;
        }

        static protected EntityState ConvertEntityState(DbState state)
        {
            switch (state)
            {
                case DbState.Added:
                    return EntityState.Added;
                case DbState.Modified:
                    return EntityState.Modified;
                case DbState.Deleted:
                    return EntityState.Deleted;
                default:
                    return EntityState.Unchanged;
            }
        }
    }
}
