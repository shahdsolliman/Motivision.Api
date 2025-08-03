using Microsoft.EntityFrameworkCore;
using Motivision.Core.Business.Entities;
using Motivision.Core.Business.Specifications;
using Motivision.Core.Contracts;
using Motivision.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motivision.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {

        private readonly AppBusinessDbContext _dbContext;

        public GenericRepository(AppBusinessDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            // return (IEnumerable<T>) await _dbContext.Set<Product>().Skip(20).Take(10).OrderBy(P=>P.Name).Include(P => P.ProductBrand).ToListAsync();
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T?> GetAsync(int id)
        {
            return await _dbContext.Set<T>().FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task AddAsync(T entity)
            => await _dbContext.Set<T>().AddAsync(entity);

        public void Update(T entity)
            => _dbContext.Set<T>().Update(entity);

        public void Delete(T entity)
            => _dbContext.Set<T>().Remove(entity);

        public async Task<T?> GetEntityWithSpecAsync(ISpecifications<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecifications<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<int> CountAsync(ISpecifications<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        private IQueryable<T> ApplySpecification(ISpecifications<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_dbContext.Set<T>().AsQueryable(), spec);
        }

    }
}
