using System.Threading.Tasks;

namespace Timecards.Application.Interfaces
{
    public interface IDatabaseInitializer
    {
        Task Seed();
    }
}