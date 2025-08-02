using Motivision.Core.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motivision.Core.Contracts
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        // 1. Property Signature for each and every (repo) table in the database 
        // Open for Extension, Closed for Modification => Generate Method

        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;

        // 2. SaveChangesAsync method

        Task<int> CompleteAsync();

        // 3. DisposeAsync method => Inherited from IAsyncDisposable
    }
}
