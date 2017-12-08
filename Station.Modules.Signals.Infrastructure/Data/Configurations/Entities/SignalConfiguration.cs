using Station.Kernel.Infrastructure.Data;
using Station.Modules.Signals.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Station.Modules.Signals.Infrastructure.Data.Configurations.Entities
{
    public class SignalConfiguration : EntityTypeConfiguration<Signal>
    {
        public override void Configure(EntityTypeBuilder<Signal> builder)
        {
            builder.ToTable("Signal");

            builder.Property(p => p.ID)
                .HasColumnName("SignalID").UseSqlServerIdentityColumn();
        }
    }
}