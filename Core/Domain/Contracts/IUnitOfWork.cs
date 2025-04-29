﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Modules;

namespace Domain.Contracts
{
    public interface IUnitOfWork
    {
        Task<int> SaveChanges();


        //IGenericRepository<Product,int> ProductRepository { get; }
        //IGenericRepository<ProductBrand,int> ProductBrandRepository { get; }
        //IGenericRepository<ProductType,int> ProductTypeRepository { get; }


        IGenericRepository<TEntity,TKey> GetRepository<TEntity,TKey>() where TEntity : BaseEntity<TKey>;

    }
}
