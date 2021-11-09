using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using Timecards.Application.Interfaces;

namespace Timecards.Application.Query.User
{
    public class GetAccountQueryHandler : IRequestHandler<GetUserQuery, IList<GetAllUsersResponse>>
    {
        private readonly IConnection _connection;

        public GetAccountQueryHandler(IConnection connection)
        {
            _connection = connection;
        }

        public async Task<IList<GetAllUsersResponse>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var searchUser = @"SELECT 
                                   u.[Id] AS [UserId],
                                   u.[UserName],
                                   u.[Email],
                                   u.[FirstName],
                                   u.[LastName]
                                   r.[Name] AS [Role]
                                FROM [AspNetUsers] AS u 
                                INNER JOIN [AspNetUserRoles] AS ur ON u.[Id] = ur.[UserId] 
                                INNER JOIN [AspNetRoles] AS r ON ur.[RoleId] = r.[Id]";
            
            using (var conn = _connection.OpenConnection())
            {
                var result = await conn.QueryAsync<GetAllUsersResponse>(searchUser);
                return result.ToList();
            }
        }
    }
}