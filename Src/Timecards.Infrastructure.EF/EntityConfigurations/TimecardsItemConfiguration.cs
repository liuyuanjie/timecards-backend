using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Timecards.Domain;

namespace Timecards.Infrastructure.EF.EntityConfigurations
{
    public class TimecardsItemConfiguration : IEntityTypeConfiguration<TimecardsItem>
    {
        public void Configure(EntityTypeBuilder<TimecardsItem> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.Hour).HasPrecision(3, 1);
            builder.Property(x => x.WorkDay);
            builder.Property(x => x.Note).HasMaxLength(256);
        }
    }
}