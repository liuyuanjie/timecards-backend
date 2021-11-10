using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Timecards.Application.Extensions;
using Timecards.Application.Interfaces;
using Timecards.Application.Model;
using Timecards.Application.Query.Timecards;

namespace Timecards.Application.Query.Project
{
    public class GetProjectQueryHandler : IRequestHandler<GetProjectQuery, IList<GetProjectResponse>>
    {
        private readonly IConnection _connection;

        public GetProjectQueryHandler(IConnection connection)
        {
            _connection = connection;
        }

        public async Task<IList<GetProjectResponse>> Handle(GetProjectQuery request,
            CancellationToken cancellationToken)
        {
            var searchProject = @"SELECT 
                                    p.[Id] AS [ProjectId],
                                    p.[Name] AS [Name],
                                    pt.[Name] AS [ParentName],
                                    p.[ProjectType] AS [ProjectType],
                                    p.[ParentProjectId]
                                FROM [Projects] AS p 
                                LEFT JOIN [Projects] AS pt ON p.[ParentProjectId] = pt.[Id]";
            
            using (var conn = _connection.OpenConnection())
            {
                var result = await conn.QueryAsync<GetProjectResponse>(searchProject);
                return result.ToList();
            }
        }
    }
}