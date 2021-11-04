using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timecards.Domain;

namespace Timecards.Application.Interfaces
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        IUnitOfWork UnitOfWork { get; }
        IQueryable<TEntity> Query();
        void Delete(TEntity aggregateRoot);
        Task CreateAsync(IEnumerable<TEntity> aggregateRoots);
    }
}