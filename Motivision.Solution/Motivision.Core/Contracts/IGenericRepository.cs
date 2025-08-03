using Motivision.Core.Business.Entities;
using Motivision.Core.Business.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motivision.Core.Contracts
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        
        Task<T?> GetAsync(int id);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);

        // Specifications
        Task<T?> GetEntityWithSpecAsync(ISpecifications<T> spec);
        Task<IReadOnlyList<T>> ListAsync(ISpecifications<T> spec);
        Task<int> CountAsync(ISpecifications<T> spec); // useful for pagination

    }
}
