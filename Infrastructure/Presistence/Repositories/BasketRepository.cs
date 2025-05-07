using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Modules.BasketModule;

namespace Presistence.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        public Task<CustomerBasket?> CreateOrUpdate(CustomerBasket basket)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<CustomerBasket?> GetAsync()
        {
            throw new NotImplementedException();
        }
    }
}
