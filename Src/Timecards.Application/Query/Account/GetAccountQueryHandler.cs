using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using Timecards.Application.Interfaces;

namespace Timecards.Application.Query.Account
{
    public class GetAccountQueryHandler : IRequestHandler<GetAccountQuery, GetAccountResponse>
    {
        private readonly IConnection _connection;

        public GetAccountQueryHandler(IConnection connection)
        {
            _connection = connection;
        }

        public async Task<GetAccountResponse> Handle(GetAccountQuery request, CancellationToken cancellationToken)
        {
            var searchQuery = @"SELECT 
                                   u.[Id] AS [AccountId],
                                   u.[UserName],
                                   u.[Email],
                                   u.[FirstName],
                                   u.[LastName],
                                   r.[Name] AS [Role]
                                FROM [AspNetUsers] AS u 
                                INNER JOIN [AspNetUserRoles] AS ur ON u.[Id] = ur.[UserId] 
                                INNER JOIN [AspNetRoles] AS r ON ur.[RoleId] = r.[Id]
                                WHERE u.[Id] = @AccountId";
            
            using (var conn = _connection.OpenConnection())
            {
                var result = await conn.QueryFirstAsync<GetAccountResponse>(searchQuery, request);
                return result;
            }
        }
    }
}