using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using Timecards.Application.Interfaces;
using Timecards.Application.Model;

namespace Timecards.Application.Query.Timecards
{
    public class SearchTimecardsQueryHandler : IRequestHandler<SearchTimecardsQuery, IList<SearchTimecardsResponse>>
    {
        private readonly IConnection _connection;

        public SearchTimecardsQueryHandler(IConnection connection)
        {
            _connection = connection;
        }

        public async Task<IList<SearchTimecardsResponse>> Handle(SearchTimecardsQuery request, CancellationToken cancellationToken)
        {
            var searchQuery = @"SELECT 
                                    t.[Id] AS [TimecardsId],
                                    u.[FirstName],
                                    u.[LastName],
                                    r.[Name] AS [Role],
                                    p.[Name] AS [ProjectName],
                                    t.[StatusType],
                                    ti.[StartDate],
                                    ti.[EndDate],
                                    ti.[TotalHour]
                                FROM [Timecardses] AS t
                                INNER JOIN (SELECT 
                                    [TimecardsId],
                                    Min([WorkDay]) AS [StartDate],
                                    MAX([WorkDay]) AS [EndDate],
                                    SUM([Hour]) AS [TotalHour] FROM [TimecardsItem] GROUP BY [TimecardsId]) AS ti ON t.[Id] = ti.[TimecardsId]
                                INNER JOIN [Projects] AS p ON p.[Id] = t.[ProjectId]
                                INNER JOIN [AspNetUsers] AS u ON u.[Id] = t.[AccountId]
                                INNER JOIN [AspNetUserRoles] AS ur ON u.[Id] = ur.[UserId] 
                                INNER JOIN [AspNetRoles] AS r ON ur.[RoleId] = r.[Id]";
            
            using (var conn = _connection.OpenConnection())
            {
                var result = await conn.QueryAsync<SearchTimecardsResponse>(searchQuery);
                return result.ToList();
            }
        }
    }
}