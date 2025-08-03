using Microsoft.EntityFrameworkCore;
using Motivision.Core.Business.Entities;
using Motivision.Core.Contracts;
using Motivision.Infrastructure.Persistence;
using Motivision.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motivision.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppBusinessDbContext _dbContext;
        private Dictionary<string, object> _repositories;
        public UnitOfWork(AppBusinessDbContext dbContext)
        {
            _dbContext = dbContext;
            _repositories = new Dictionary<string, object>();
        }
        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            var key = typeof(TEntity).Name;
            if (!_repositories.ContainsKey(key))
            {
                var repository = new GenericRepository<TEntity>(_dbContext) /*as GenericRepository<BaseEntity>*/;
                _repositories.Add(key, repository);  // warning because [as] is explicitly casted
            }
            return (IGenericRepository<TEntity>)_repositories[key];
        }

        public async Task<int> CompleteAsync()
            => await _dbContext.SaveChangesAsync();

        public async ValueTask DisposeAsync()
            => await _dbContext.DisposeAsync();
    }
}
