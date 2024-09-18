using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interface;
using Microsoft.AspNetCore.Mvc;

namespace APIBackend.Controllers
{
    public class BasketController : BaseAPIController
    {
        private readonly IBasketRepository basketRepository;
        public BasketController(IBasketRepository _basketRepository)
        {
            this.basketRepository = _basketRepository;
        }
        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasketbyID(string Id){
            var basket = await basketRepository.GetBasketAsync(Id);
            return Ok(basket ?? new CustomerBasket());
        }
        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket customerBasket){
            var updatebaseket=await basketRepository.UpdateBasketAsync(customerBasket);
            return Ok(updatebaseket);
        }
        [HttpDelete]
        public async Task DeleteBasketAsync(string Id){
            await basketRepository.DeleteBasketAsync(Id);
        }
    }
}