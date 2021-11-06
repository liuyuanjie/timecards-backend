using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Timecards.Application.Interfaces;
using Timecards.Domain;
using Timecards.Domain.Enum;

namespace Timecards.Infrastructure.EF
{
    public class DatabaseInitializer : IDatabaseInitializer
    {
        private readonly TimecardsDbContext _dbContext;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly IRepository<Project> _projectRepository;

        public DatabaseInitializer(TimecardsDbContext dbContext, RoleManager<IdentityRole<Guid>> roleManager,
            IRepository<Project> projectRepository)
        {
            _dbContext = dbContext;
            _roleManager = roleManager;
            _projectRepository = projectRepository;
        }

        public async Task Seed()
        {
            await AddRoles();
            await AddProjects();
        }

        private async Task AddProjects()
        {
            if (_dbContext.Set<Project>().Any())
            {
                return;
            }
            
            var globalProject = Project.CreateProject("Internal Global", ProjectType.Global, null);
            var customProject = Project.CreateProject("Custom", ProjectType.Global, null);
            await _dbContext.Projects.AddRangeAsync(new[]
            {
                globalProject,
                customProject,
                new Project {Name = "Google", ProjectType = ProjectType.Custom, ParentProjectId = customProject.Id},
                new Project {Name = "Facebook", ProjectType = ProjectType.Custom, ParentProjectId = customProject.Id},
                new Project {Name = "Microsoft", ProjectType = ProjectType.Custom, ParentProjectId = customProject.Id},
                new Project {Name = "Alibaba", ProjectType = ProjectType.Custom, ParentProjectId = customProject.Id},
                new Project
                {
                    Name = "Asking for Time Off", ProjectType = ProjectType.Global, ParentProjectId = globalProject.Id
                },
                new Project
                {
                    Name = "Ask For A Sick Leave", ProjectType = ProjectType.Global, ParentProjectId = globalProject.Id
                },
            });
            await _dbContext.SaveChangesAsync();
        }

        private async Task AddRoles()
        {
            if (_roleManager.Roles.Any())
            {
                return;
            }
            
            await _roleManager.CreateAsync(new IdentityRole<Guid>("Admin"));
            await _roleManager.CreateAsync(new IdentityRole<Guid>("Staff"));
        }
    }
}