using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Timecards.Infrastructure.EF.EntityConfigurations
{
    public class TimecardsConfiguration : IEntityTypeConfiguration<Domain.Timecards>
    {
        public void Configure(EntityTypeBuilder<Domain.Timecards> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.ProjectId).IsRequired();
            builder.Property(x => x.AccountId).IsRequired();
            builder.Property(x => x.TimecardsDate).IsRequired();
            builder.HasMany(b => b.Items)
                .WithOne()
                .HasForeignKey(nameof(Domain.Timecards.Id))
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}