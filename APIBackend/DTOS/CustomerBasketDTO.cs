using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APIBackend.DTOS
{
    public class CustomerBasketDTO
    {
        [Required]
         public string Id { set; get; }
        public List<BasketItemDTO> Items { set; get; } 
    }
}