using MongoDB.Driver;
using Station.Kernel.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Station.Modules.Signals.Infrastructure.Data
{
    public class SignalMongoDB : KernelMongoClient
    {
        ////public StationDbContext()
        ////    : base(new DbContextOptions<CommonDbContext>())
        ////{ }

        ////public StationDbContext(DbContextOptions<CommonDbContext> options)
        ////    : base(options)
        ////{ }

        ////protected override void OnModelCreating(ModelBuilder modelBuilder)
        ////{
        ////    //Entities
        ////    modelBuilder.ApplyConfiguration(new SignalConfiguration());

        ////    base.OnModelCreating(modelBuilder);

        ////}
    }
}
