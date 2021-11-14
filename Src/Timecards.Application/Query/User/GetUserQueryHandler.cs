using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using Timecards.Application.Interfaces;

namespace Timecards.Application.Query.User
{
    public class GetAccountQueryHandler : IRequestHandler<GetUserQuery, IList<GetUserResponse>>
    {
        private readonly IConnection _connection;

        public GetAccountQueryHandler(IConnection connection)
        {
            _connection = connection;
        }

        public async Task<IList<GetUserResponse>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var searchQuery = @"SELECT 
                                   u.[Id] AS [UserId],
                                   u.[UserName],
                                   u.[Email],
                                   u.[FirstName],
                                   u.[LastName],
                                   r.[Name] AS [Role]
                                FROM [AspNetUsers] AS u 
                                INNER JOIN [AspNetUserRoles] AS ur ON u.[Id] = ur.[UserId] 
                                INNER JOIN [AspNetRoles] AS r ON ur.[RoleId] = r.[Id]";

            List<string> whereClause = new List<string>();
            if (!string.IsNullOrWhiteSpace(request.Email))
            {
                whereClause.Add("u.[Email] = @email");
            }

            if (!string.IsNullOrWhiteSpace(request.FirstName))
            {
                whereClause.Add("u.[FirstName] LIKE @firstName");
            }

            if (!string.IsNullOrWhiteSpace(request.LastName))
            {
                whereClause.Add("u.[LastName] LIKE @lastName");
            }

            if (whereClause.Any())
            {
                searchQuery += $" WHERE {string.Join(" AND ", whereClause)} ";
            }

            using (var conn = _connection.OpenConnection())
            {
                var result = await conn.QueryAsync<GetUserResponse>(searchQuery, new
                {
                    Email = request.Email,
                    FirstName = $"%{request.FirstName}%",
                    LastName = $"%{request.LastName}%",
                });
                return result.ToList();
            }
        }
    }
}