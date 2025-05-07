using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Modules.BasketModule
{
    public class CustomerBasket
    {
        public string Id { get; set; } //Guid : Generated From Client Side
        public IEnumerable<BasketItem> Items { get; set; }
    }
}
