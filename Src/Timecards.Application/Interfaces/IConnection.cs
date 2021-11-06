using System.Data;

namespace Timecards.Application.Interfaces
{
    public interface IConnection
    {
        IDbConnection OpenConnection();
    }
}