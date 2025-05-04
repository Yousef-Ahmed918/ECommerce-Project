using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface ISpecifications<T> where T : class
    {
        //Where
        //_StoreDbContext.Set<T>.Where(Expression<Func<T,bool>>)
        //_StoreDbContext.Set<T>.Select(Expression<Func<T,Object>>)

        //1)Criteria 
        Expression<Func<T, bool>> Criteria { get; } //For Filtering

        //Include
        List<Expression<Func<T, object>>> IncludeExpressions { get; } //For Filtering

        //Include in Eager Loading 
        //_StoreDbContext.Set<T>.Where(Specification.Criteria).Include(IncludeExpressions[0])

        //OrderBy 
        Expression<Func<T, object>> OrderBy { get; } //For Order Asc
        Expression<Func<T, object>> OrderByDesc { get; } //For Order Desc
        
        int Skip {  get; }
        int Take { get; }
        bool IsPaginated { get; }
    }
}
