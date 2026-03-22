using Gizli.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gizli.Infrastructure.Configurations
{
    internal sealed class AlarmLogConfiguration : IEntityTypeConfiguration<AlarmLog>
    {
        public void Configure(EntityTypeBuilder<AlarmLog> builder)
        {
            builder.ToTable("AlarmLogs");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Temperature).HasColumnType("float");
            builder.Property(p => p.CreatedAt).HasColumnType("datetime2");
        }
    }
}
