using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class CustomerBasket
    {
        public CustomerBasket()
        {
        }
        public CustomerBasket(string id)
        {
            this.Id = id;
        }
        public string Id { set; get; }
        public List<BasketItem> Items { set; get; } = new List<BasketItem>();
    }
}
