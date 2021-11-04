using Timecards.Domain.Enum;

namespace Timecards.Domain
{
    public class Project: EntityBase
    {
        public string Name { get; set; }
        public ProjectType ProjectType { get; set; }

        public static Project CreateProject(string name, ProjectType projectType)
        {
            return new Project
            {
                Name = name,
                ProjectType = projectType
            };
        }
    }
}