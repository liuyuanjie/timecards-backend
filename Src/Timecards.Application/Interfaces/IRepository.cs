using System.Linq;
using Timecards.Domain;

namespace Timecards.Application.Interfaces
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        IUnitOfWork UnitOfWork { get; }
        IQueryable<TEntity> Query();
        void Delete(TEntity entity);
        void Create(TEntity entity);
    }
}