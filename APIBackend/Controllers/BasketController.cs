
using APIBackend.DTOS;
using AutoMapper;
using Core.Entities;
using Core.Interface;
using Microsoft.AspNetCore.Mvc;

namespace APIBackend.Controllers
{
    public class BasketController : BaseAPIController
    {
        private readonly IBasketRepository basketRepository;
        private readonly IMapper mapper;
        public BasketController(IBasketRepository _basketRepository,IMapper _mapper)
        {
            this.mapper = _mapper;
            this.basketRepository = _basketRepository;
        }
        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasketbyID(string Id){
            var basket = await basketRepository.GetBasketAsync(Id);
            return Ok(basket ?? new CustomerBasket());
        }
        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDTO basket){
            var customerbasket=mapper.Map<CustomerBasketDTO,CustomerBasket> (basket);
            
            var updatebaseket=await basketRepository.UpdateBasketAsync(customerbasket);
            return Ok(updatebaseket);
        }
        [HttpDelete]
        public async Task DeleteBasketAsync(string Id){
            await basketRepository.DeleteBasketAsync(Id);
        }
    }
}