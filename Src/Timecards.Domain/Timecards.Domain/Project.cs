using System;
using Timecards.Domain.Enum;

namespace Timecards.Domain
{
    public class Project : EntityBase
    {
        public string Name { get; set; }
        public ProjectType ProjectType { get; set; }

        public Guid? ParentProjectId { get; set; }

        public static Project CreateProject(string name, ProjectType projectType, Guid? parentProjectId)
        {
            return new Project
            {
                Name = name,
                ProjectType = projectType,
                ParentProjectId = parentProjectId
            };
        }
    }
}