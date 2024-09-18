using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class BasketItem
    {
        public int Id { get; set; } 
        public string ProductName{set;get;}
        public decimal Price{set;get;}  
        public int Quantity{set;get;}
        public string ProductUrl{set;get;}
        public string Brand{set;get;}
        public string Type{set;get;}
    }
}