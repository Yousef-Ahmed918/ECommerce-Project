using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;

namespace Services.Specifications
{
    public abstract  class BaseSpecifications<T> : ISpecifications<T> where T : class
    {
        public BaseSpecifications(Expression<Func<T, bool>> _criteria) //Filtering
        {
            Criteria = _criteria;
        }

        public Expression<Func<T, bool>> Criteria { get; private set; }

        public List<Expression<Func<T, object>>> IncludeExpressions { get; } = []; //Loading Nav

        public Expression<Func<T, object>> OrderBy {get;private set;}

        public Expression<Func<T, object>> OrderByDesc { get; private set; }

       

        protected void AddInclude(Expression<Func<T, object>> expression)
        {
            IncludeExpressions.Add(expression);
        }

        protected void AddOrderBy(Expression<Func<T, object>> orderBy)
        {
           OrderBy=orderBy; //p=>p.price
        }
        //OrderBy Desc
        protected void AddOrderByDesc(Expression<Func<T, object>> orderByDesc)
        {
            OrderBy = orderByDesc; //p=>p.price
        }

        public int Skip { get; private set; }

        public int Take {  get; private set; }

        public bool IsPaginated {  get; private set; }

        //Apply Pagination
        protected void ApplyPagination(int PageSize,int PageIndex)
        {
            IsPaginated= true;
            Take = PageSize;
            Skip = (PageIndex - 1) * PageSize;           //(PageIndex-1)*pageSize
        } 
    }
}
