using Station.Common.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace Station.Kernel.Infrastructure.Data
{
    public partial class KernelDbContext : CommonDbContext
    {
        public KernelDbContext()
            : base()
        { }

        public KernelDbContext(DbContextOptions<CommonDbContext> options)
            : base(options) { }
    }
}