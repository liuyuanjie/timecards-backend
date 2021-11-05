using System.Linq;
using Timecards.Application.Interfaces;
using Timecards.Domain;

namespace Timecards.Infrastructure.EF
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly TimecardsDbContext _dbContext;

        public GenericRepository(TimecardsDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public IUnitOfWork UnitOfWork => _dbContext;
        
        public IQueryable<TEntity> Query()
        {
            return _dbContext.Set<TEntity>();
        }

        public void Create(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
        }

        public void Delete(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }
    }
}