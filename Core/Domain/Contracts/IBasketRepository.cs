using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Modules.BasketModule;

namespace Domain.Contracts
{
    public interface IBasketRepository
    {
        Task<CustomerBasket?> GetAsync();
        //Create basket
        Task<CustomerBasket?> CreateOrUpdate(CustomerBasket basket);

        //Remove item from basket 
        Task DeleteAsync(string id);
    }
}
