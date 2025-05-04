using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Presistence.Repositories
{
    public static class SpecificationEvaluator
    {
        public static IQueryable<T> CreateQuery<T>(IQueryable<T> inputQuery,ISpecifications<T> specifications) where T : class 
        {
            var query = inputQuery;
                if (specifications.Criteria is not null)
            {
                query=query.Where(specifications.Criteria);
            }

            if (specifications.OrderBy is not null)
            {
                query = query.OrderBy(specifications.OrderBy);
            } 
            else if (specifications.OrderByDesc is not null)
            {
                query = query.OrderByDescending(specifications.OrderByDesc);
            }
            if (specifications.IsPaginated)
            {
                query=query.Skip(specifications.Skip).Take(specifications.Take);
            }

            foreach (var include in specifications.IncludeExpressions)
            {
                query.Include(include);
            }
            //OR
            //query = specifications.IncludeExpressions.
            //    Aggregate(query, (CurrentQuery, include) => CurrentQuery.Include(include));
            
            return query;
        }
    }
}
