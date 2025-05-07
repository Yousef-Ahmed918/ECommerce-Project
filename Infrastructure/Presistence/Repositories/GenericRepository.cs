using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Modules;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Presistence.Data;

namespace Presistence.Repositories
{
    internal class GenericRepository<TEntity, TKey>(StoredDbContext _dbContext) :
        IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
           return await _dbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(TKey id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }
        public void Add(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
        }
        public void Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
        }

        public void Delete(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity> specifications)
        {
             var result = await SpecificationEvaluator.
                CreateQuery(_dbContext.Set<TEntity>(), specifications)
                .ToListAsync();
            return result; 
        }

        public async Task<TEntity?> GetByIdAsync(ISpecifications<TEntity> specifications)
        {
            var result =await SpecificationEvaluator.
                CreateQuery(_dbContext.Set<TEntity>(), specifications)
                .FirstOrDefaultAsync();
            return result;
        }

        public async Task<int> CountAsync(ISpecifications<TEntity> specifications)=>
           await SpecificationEvaluator.CreateQuery(_dbContext.Set<TEntity>(),specifications).CountAsync();
        
    }
}
