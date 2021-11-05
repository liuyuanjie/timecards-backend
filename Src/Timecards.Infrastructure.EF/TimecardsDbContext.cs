using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Timecards.Application.Interfaces;
using Timecards.Domain;
using Timecards.Infrastructure.EF.EntityConfigurations;

namespace Timecards.Infrastructure.EF
{
    public class TimecardsDbContext : IdentityDbContext<Account, IdentityRole<Guid>, Guid>, IUnitOfWork
    {
        public TimecardsDbContext(DbContextOptions<TimecardsDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ProjectConfiguration());
            builder.ApplyConfiguration(new TimecardsConfiguration());
            builder.ApplyConfiguration(new TimecardsItemConfiguration());
            base.OnModelCreating(builder);
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Domain.Timecards> Timecardses { get; set; }

        public async Task<int> CommitAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await this.SaveChangesAsync(cancellationToken);
        }
    }
}