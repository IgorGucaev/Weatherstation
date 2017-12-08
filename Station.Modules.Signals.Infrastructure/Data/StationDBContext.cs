using Station.Common.Infrastructure.Data;
using Station.Kernel.Infrastructure.Data;
using Station.Modules.Signals.Infrastructure.Data.Configurations.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Station.Modules.Signals.Infrastructure.Data
{
    public partial class StationDbContext : KernelDbContext
    {
        public StationDbContext()
            : base(new DbContextOptions<CommonDbContext>())
        { }

        public StationDbContext(DbContextOptions<CommonDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Entities
            modelBuilder.ApplyConfiguration(new SignalConfiguration());

            base.OnModelCreating(modelBuilder);

        }
    }
}