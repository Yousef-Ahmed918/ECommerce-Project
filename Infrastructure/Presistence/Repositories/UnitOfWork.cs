using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Modules;
using Presistence.Data;

namespace Presistence.Repositories
{
    public class UnitOfWork(StoredDbContext _dbContext) : IUnitOfWork
    {

        //Inject IUnitOfWork _unitOfWork
        //_unitOfWork.GetRepository<Product,int>();

        private readonly Dictionary<string, object> _repositories = [];

        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            var typeName= typeof(TEntity).Name; //ex (Product)
            if (_repositories.ContainsKey(typeName) ) 
                return(GenericRepository<TEntity, TKey>) _repositories[typeName]; //explict casting

            var repo = new GenericRepository<TEntity, TKey>(_dbContext);
            _repositories[typeName] = repo;
            return repo;
        }

        public async Task<int> SaveChanges()
        {
           return await _dbContext.SaveChangesAsync();
        }
    }
}
