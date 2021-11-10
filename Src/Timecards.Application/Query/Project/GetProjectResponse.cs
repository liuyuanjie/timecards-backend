using System;
using Timecards.Domain.Enum;

namespace Timecards.Application.Query.Project
{
    public class GetProjectResponse
    {
        public Guid ProjectId { get; set; }
        public string Name { get; set; }
        public string ParentName { get; set; }
        public ProjectType ProjectType { get; set; }        
        public Guid? ParentProjectId { get; set; }
    }
}